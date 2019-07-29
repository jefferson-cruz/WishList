using System;
using System.Collections.Generic;
using System.Text;

namespace WishList.Shared.Repositories.Interfaces
{
    public interface IConnectionStringManager
    {
        IReadOnlyDictionary<ConnectionStrings, string> ConnectionStrings { get; }
    }
}
