namespace OutdoorShop.Catalog.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.Azure.Documents;

    public interface IDocumentRepository<TDocument> where TDocument : DocumentBase
    {
        Task<Document> CreateAsync(TDocument document);
        Task DeleteAsync(string id);
        Task<TDocument> GetAsync(string id);
        Task<IEnumerable<TDocument>> GetAllAsync();
        Task<IEnumerable<TDocument>> GetAllAsync(params string[] ids);
        Task<IEnumerable<TDocument>> GetAsync(Expression<Func<TDocument, bool>> predicate);
        Task<Document> UpdateAsync(TDocument document);
    }
}
