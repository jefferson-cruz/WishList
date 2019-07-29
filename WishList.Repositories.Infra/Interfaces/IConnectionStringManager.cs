using System.Collections.Generic;
using WishList.Repositories.Context;

namespace WishList.Repositories.Infra.Interfaces
{
    public interface IConnectionStringManager
    {
        IDictionary<ConnectionStringsEnum, string> ConnectionStrings { get; }
    }
}
