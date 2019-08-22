using System.Threading.Tasks;
using WishList.Shared.Result;

namespace WishList.Services.Interfaces
{
    public interface IIndexService<TModel> where TModel : class
    {
        /// <summary>
        /// Index a document
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> IndexDocumentAsync(TModel model);

        /// <summary>
        /// Update a document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> UpdateDocumentAsync(int id, TModel model);

        /// <summary>
        /// Delete a document
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> DeleteDocumentAsync(int id);
    }
}
