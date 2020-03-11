namespace OutdoorShop.Catalog.Api.FeaturedProduct
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("featuredproducts")]
    [ApiController]
    [ApiVersion("1.0")]
    public class Controller : ControllerBase
    {
        private readonly IMediator mediator;

        public Controller(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Gets all featured products
        /// </summary>
        [HttpGet("")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<GetAll.Model>>> GetAllAsync()
        {
            var products = await mediator.Send(new GetAll.Query());

            return Ok(products.ToList());
        }
    }
}