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
        public string IndexName => this.context.GetIndexName<TModel>();

        /// <summary>
        /// Add document
        /// </summary>
        /// <param name="model"></param>
        /// <param name="idModel"></param>
        /// <param name="indexName"></param>
        public async Task<bool> IndexDocumentAsync(TModel model)
        {
            var response = await this.context.IndexAsync(new IndexRequest<TModel>(model, this.IndexName));

            return response.IsValid;
        }

        /// <summary>
        /// Update document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task DeleteDocumentAsync(int id)
        {
            await context.DeleteAsync(DocumentPath<TModel>.Id(id), x => x
                .Index(IndexName)
            );
        }
    }
}
