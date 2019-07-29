using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using WishList.Repositories.Infra.Interfaces;

namespace WishList.Repositories.Infra
{
    public class ConnectionStringManager  : IConnectionStringManager
    {
        public IDictionary<ConnectionStringsEnum, string> ConnectionStrings { get; }
        
        public ConnectionStringManager(IConfigurationSection configurationSection)
        {
            ConnectionStrings = LoadConnectionStrings(configurationSection);
        }

        private IDictionary<ConnectionStringsEnum, string> LoadConnectionStrings(IConfigurationSection configurationSection)
        {
            var connectionStrings = new Dictionary<ConnectionStringsEnum, string>();

            foreach (var connectionString in configurationSection.GetChildren())
            {
                connectionStrings.Add(Enum.Parse<ConnectionStringsEnum>(connectionString.Key), connectionString.Value);
            }

            return connectionStrings;
        }
    }
}
