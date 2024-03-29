﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace WishList.Repositories.Index.Interfaces
{
    public interface IIndexRepository<TModel> where TModel : class
    {
        string IndexName { get; }
        Task<bool> IndexDocumentAsync(TModel model);
        Task<bool> UpdateDocumentAsync(int id, TModel model);
        Task DeleteDocumentAsync(int id);
    }
}