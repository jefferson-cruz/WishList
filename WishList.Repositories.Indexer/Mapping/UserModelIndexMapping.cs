using Nest;
using WishList.Repositories.Index.Context;
using WishList.Services.Models.User;

namespace WishList.Repositories.Index.Mapping
{
    public class UserModelIndexMapping
    {
        public UserModelIndexMapping(IndexContext context)
        {
            context.Indices.Create(context.GetIndexName<UserModel>(), c => c
                .Map<UserModel>(m => m
                    .Properties(p => p
                        .Number(nm => nm.Name(n => n.Id).Type(NumberType.Integer))
                        .Text(tx => tx.Name(n => n.Name))
                        .Text(tx => tx.Name(n => n.Email).Analyzer("keyword"))
                    )
                )
            );
        }
    }
}
