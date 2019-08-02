using System;
using System.Collections.Generic;
using System.Linq;
using WishList.Shared.Notify.Notifications;
using WishList.Shared.Result;

namespace WishList.Shared.Notify
{
    /// <summary>
    /// Class to notifications management. Used to propagate messages to differents componentes of system
    /// </summary>
    public class Notify
    {
        private readonly List<IResultBase> results = new List<IResultBase>();

        public IReadOnlyCollection<IResultBase> Results => this.results;

        public bool HasResults => this.results.Any();

        public bool HasNoResults => !HasResults;

        public void AddResult(IResultBase result) => this.results.Add(result);

        public void AddResults(IReadOnlyCollection<IResultBase> results) => this.results.AddRange(results);
    }
}
