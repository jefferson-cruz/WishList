using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace WishList.Shared.Repositories
{
    public class ConnectionStringManager 
    {
        private Dictionary<ConnectionStrings, string> connectionStrings;

        public IReadOnlyDictionary<ConnectionStrings, string> ConnectionStrings => this.connectionStrings;

        public ConnectionStringManager(IConfigurationSection configurationSection)
        {
            this.connectionStrings = LoadConnectionStrings(configurationSection);
        }

        private Dictionary<ConnectionStrings, string> LoadConnectionStrings(IConfigurationSection configurationSection)
        {
            var connectionStrings = new Dictionary<ConnectionStrings, string>();

            foreach (var connectionString in configurationSection.GetChildren())
            {
                connectionStrings.Add(Enum.Parse<ConnectionStrings>(connectionString.Key), connectionString.Value);
            }

            return connectionStrings;
        }
    }
}
