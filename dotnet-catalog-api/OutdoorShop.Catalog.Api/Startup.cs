namespace OutdoorShop.Catalog.Api
{
    using System;
    using System.IO;
    using System.Reflection;
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using OutdoorShop.Catalog.Domain;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

#pragma warning disable CS1591
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AzureAccountDetails>(Configuration.GetSection(nameof(AzureAccountDetails)));

            services.AddOptions();
            services.AddControllers();
            services.AddApiVersioning(x => 
                {
                    x.ReportApiVersions = true;
                    x.ApiVersionReader = new HeaderApiVersionReader("api-version");
                });

            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            services.AddVersionedApiExplorer(x => {
                x.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                x.SubstituteApiVersionInUrl = true;
            });
            
            // register the swagger generator
            services.AddSwaggerGen(x =>
            {
                // note: need a temporary service provider here because one has not been created yet
#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'

                // add a swagger document for each discovered API version
                foreach ( var description in provider.ApiVersionDescriptions )
                {
                    x.SwaggerDoc( description.GroupName, CreateInfoForApiVersion( description ) );
                }

                x.CustomSchemaIds(c => SchemaIdStrategy(c));
                //x.TagActionsBy(c => GetActionTags(c));
                x.OperationFilter<TagByApiExplorerSettingsOperationFilter>();

                x.IgnoreObsoleteProperties();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = ($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                var xmlPath = Path.Combine(AppContext.BaseDirectory.ToLowerInvariant(), xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            AddApplicationServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper autoMapper, IApiVersionDescriptionProvider versionProvider )
        {
            app.UseStaticFiles();

            app.UseSwagger(x =>
            {
                x.RouteTemplate = "api-docs/{documentName}/openapi.json";
            });
            //enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            //specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(x =>
            {
                x.DocumentTitle = "OutdoorShop Catalog API";
                // build a swagger endpoint for each discovered API version
                foreach ( var description in versionProvider.ApiVersionDescriptions )
                {
                    x.SwaggerEndpoint($"/api-docs/{description.GroupName}/openapi.json", description.GroupName.ToUpperInvariant());
                }
                x.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                autoMapper.ConfigurationProvider.AssertConfigurationIsValid();
                app.UseCors(builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            }

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void AddApplicationServices(IServiceCollection services)
        {
            string connectionString = this.Configuration["postgres-user"];
                        
            services.AddMediatR(typeof(Startup).Assembly, typeof(Entity).Assembly);
            services.AddAutoMapper(typeof(Startup));
            
            services.AddSingleton<DocumentClient>(x => {
                var options = x.GetService<IOptions<AzureAccountDetails>>();
                return new DocumentClient(new Uri(options.Value.CosmosDbEndpoint), options.Value.CosmosDbKey, serializerSettings: new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            });
            services.AddTransient(typeof(IDocumentRepository<>), typeof(DocumentRepository<>));
            services.AddDbContext<CategoryContext>(options => {
                options.UseNpgsql(connectionString);
                options.UseLoggerFactory(ConsoleLoggerFactory);
            });
        }

        static OpenApiInfo CreateInfoForApiVersion( ApiVersionDescription description )
        {
            var info = new OpenApiInfo()
            {
                Title = $"Outdoor Shop Catalog API",
                Version = description.ApiVersion.ToString(),
                Description = "A sample application with Swagger, Swashbuckle, and API versioning.",
                Contact = new OpenApiContact() { Name = "Joe Young", Email = "joe@joeyoung.net" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };
            
            if ( description.IsDeprecated )
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

        static string SchemaIdStrategy(Type currentClass)
        {
            var dataContractAttribute = currentClass.GetCustomAttribute<DataContractAttribute>();
            return dataContractAttribute != null && dataContractAttribute.Name != null ? dataContractAttribute.Name : currentClass.Name;
        }


        public class TagByApiExplorerSettingsOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    var apiExplorerSettings = controllerActionDescriptor
                        .ControllerTypeInfo.GetCustomAttributes(typeof(ApiExplorerSettingsAttribute), true)
                        .Cast<ApiExplorerSettingsAttribute>().FirstOrDefault();

                    if (apiExplorerSettings != null && !string.IsNullOrWhiteSpace(apiExplorerSettings.GroupName))
                    {
                        operation.Tags = new List<OpenApiTag> { new OpenApiTag { Name = apiExplorerSettings.GroupName } };
                    }
                    else
                    {
                        operation.Tags = new List<OpenApiTag> { new OpenApiTag { Name = controllerActionDescriptor.ControllerName } };
                    }
                }
            }
        }
    }
#pragma warning restore CS1591
}
