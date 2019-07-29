using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishList.Repositories.Index.Context;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Services.Models.Common;
using WishList.Services.Models.User;

namespace WishList.Repositories.ReadOnly
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly IndexContext context;

        public UserQueryRepository(IndexContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<UserModel>> GetAll(PaginationModel paginationModel)
        {
            var search = await context.SearchAsync<UserModel>(x => x
                .From(paginationModel.Offset)
                .Size(paginationModel.PageSize)
                .Query(q => q.MatchAll())
                .Index(context.GetIndexName<UserModel>()));

            return search.Documents;
        }

        public async Task<UserModel> GetById(int id)
        {
            var search = await context.SearchAsync<UserModel>(x => x
               .Index(context.GetIndexName<UserModel>())
               .Query(q => q.Term(t => t.Field("id").Value(id))
            ));

            return search.Documents.Count > 0 ? search.Documents.First() : null;
        }

        public async Task<bool> UserExists(string email)
        {
            var search = await context.SearchAsync<UserModel>(x => x
               .Index(context.GetIndexName<UserModel>())
               .Query(q => q
                    .Match(m => m.
                        Field("email").Query(email)
                    )
                )
            );

            return search.Documents.Count > 0;
        }

        public async Task<bool> UserExists(int id)
        {
            var search = await context.SearchAsync<UserModel>(x => x
               .Index(context.GetIndexName<UserModel>())
               .Query(q => q.Term(t => t.Field("id").Value(id))
           ));

            return search.Documents.Count > 0;
        }
    }
}
