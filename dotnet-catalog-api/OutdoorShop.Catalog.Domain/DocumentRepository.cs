namespace OutdoorShop.Catalog.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.Documents.Linq;

    public class DocumentRepository<TDocument> : IDocumentRepository<TDocument> where TDocument : DocumentBase
    {
        private readonly DocumentClient client;
        private readonly string DatabaseId = "Catalog";
        private readonly string CollectionId = CollectionIdHelper.GetId(typeof(TDocument));

        public DocumentRepository(DocumentClient client)
        {
            this.client = client;

            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }
        
        public async Task<Document> CreateAsync(TDocument document)
        {
            return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), document);
        }

        public async Task DeleteAsync(string id)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
        }

        public async Task<TDocument> GetAsync(string id)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
                
                return (TDocument)(dynamic)document;
            }
            catch (DocumentClientException docEx)
            {
                if (docEx.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }               
                throw; 
            }
        }

        public async Task<IEnumerable<TDocument>> GetAllAsync()
        {
            var query = client.CreateDocumentQuery<TDocument>(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), 
                new FeedOptions {
                    MaxItemCount = 1
                })
                .AsDocumentQuery();

            List<TDocument> list = new List<TDocument>();

            while(query.HasMoreResults)
            {
                list.AddRange(await query.ExecuteNextAsync<TDocument>());
            }

            return list;
        }

        public Task<IEnumerable<TDocument>> GetAllAsync(params string[] ids)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TDocument>> GetAsync(Expression<Func<TDocument, bool>> predicate)
        {
            var query = client.CreateDocumentQuery<TDocument>(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), 
                new FeedOptions {
                    MaxItemCount = 1
                }).Where(predicate)
                .AsDocumentQuery();

            List<TDocument> list = new List<TDocument>();

            while(query.HasMoreResults)
            {
                list.AddRange(await query.ExecuteNextAsync<TDocument>());
            }

            return list;
        }

        public async Task<Document> UpdateAsync(TDocument document)
        {
            var accessCondition = new AccessCondition {
                Condition = document.ETag,
                Type = AccessConditionType.IfMatch
            };

            return await client.ReplaceDocumentAsync(
                UriFactory.CreateDocumentUri(DatabaseId, CollectionId, document.Id), 
                document,
                new RequestOptions { AccessCondition = accessCondition });
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException docEx)
            {
                if (docEx.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException docEx)
            {
                if (docEx.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 100 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}