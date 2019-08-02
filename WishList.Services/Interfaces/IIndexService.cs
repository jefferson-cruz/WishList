using System.Threading.Tasks;
using WishList.Shared.Result;

namespace WishList.Services.Interfaces
{
    public interface IIndexService<TModel> : IBaseService where TModel : class
    {
        /// <summary>
        /// Index a document
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultBase> IndexDocumentAsync(TModel model);

        /// <summary>
        /// Update a document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdateDocumentAsync(int id, TModel model);

        /// <summary>
        /// Delete a document
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteDocumentAsync(int id);
    }
}
