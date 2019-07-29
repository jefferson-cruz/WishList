using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Repositories.Index.Context;
using WishList.Repositories.Index.Interfaces;

namespace WishList.Repositories.Index
{
    public class IndexRepository<TModel> : IIndexRepository<TModel> where TModel : class
    {
        private readonly IndexContext context;

        public IndexRepository(IndexContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Suggested index name
        /// </summary>
        public string IndexName => typeof(TModel).Name.ToLower();

        /// <summary>
        /// Add document
        /// </summary>
        /// <param name="model"></param>
        /// <param name="idModel"></param>
        /// <param name="indexName"></param>
        public async Task<bool> IndexDocumentAsync(TModel model, string indexName = null)
        {
            var response = await this.context.IndexAsync(new IndexRequest<TModel>(model, string.IsNullOrEmpty(indexName) ? this.IndexName : indexName));

            return response.IsValid;
        }

        public async Task<bool> UpdateDocumentAsync(int id, TModel model)
        {
            var response = await this.context.UpdateAsync(DocumentPath<object>.Id(id), x => x
                .Index(this.context.GetIndexName<TModel>())
                .DocAsUpsert()
                .Doc(model)
                .Refresh(new Elasticsearch.Net.Refresh())
            );

            return response.IsValid;
        }

        public async Task CreateIndexIfNotExistsAsync(TModel model)
        {
            if (!await this.context.IndexExists<TModel>())
                await this.context.CreateIndex<TModel>();
        }

        public async Task<IEnumerable<TModel>> GetAllDocuments(int offset, int pageSize)
        {
            var search = await context.SearchAsync<TModel>(x => x
                .From(offset)
                .Size(pageSize)
                .Query(q => q.MatchAll())
                .Index(this.context.GetIndexName<TModel>()));

            return search.Documents;
        }

        public async Task DeleteDocumentAsync(int id, TModel model)
        {
            await context.DeleteAsync(DocumentPath<TModel>.Id(id), x => x
                .Index(IndexName)
            );
        }
    }
}
