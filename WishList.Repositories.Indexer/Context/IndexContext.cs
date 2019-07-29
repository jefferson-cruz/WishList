using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Repositories.Index.Mapping;

namespace WishList.Repositories.Index.Context
{
    public class IndexContext : ElasticClient
    {
        public IndexContext(string connectionUrl) : base(CreateConnection(connectionUrl))
        {

        }

        public async Task CreateIndex<T>()
        {
            var response = await base.Indices.CreateAsync
            (
                new CreateIndexRequest
                (
                    Nest.Indices.Index
                    (
                        GetIndexName<T>()
                    )
                )
            );

            ThrowExceptionIfNotValid(response);
        }

        public async Task<bool> IndexExists<T>()
        {
            var response = await base.Indices.ExistsAsync
            (
                new IndexExistsRequest
                (
                    Nest.Indices.Index
                    (
                        GetIndexName<T>()
                    )
                )
            );

            ThrowExceptionIfNotValid(response);

            return response.Exists;
        }

        public void DeleteIndex<T>()
        {
            var response = base.Indices.Delete
            (
                new DeleteIndexRequest
                (
                    Nest.Indices.Index
                    (
                        GetIndexName<T>()
                    )
                )
            );

            if (response.ServerError?.Error.Type != "index_not_found_exception")
                ThrowExceptionIfNotValid(response);
        }

        public void BulkInsert<T>(IEnumerable<T> objects) where T : class
        {
            var response = base.Bulk(x => x.IndexMany(objects, (i, item) =>
            {
                return i.Document(item).Index(GetIndexName<T>());
            }));

            ThrowExceptionIfNotValid(response);
        }

        public void ExecuteMappings()
        {
            new ProductModelIndexMapping(this);
            new UserModelIndexMapping(this);
            new WishModelIndexMapping(this);
        }

        public string GetIndexName<T>() => typeof(T).Name.ToLower();

        private void ThrowExceptionIfNotValid(ResponseBase response)
        {
            if (!response.IsValid)
                throw new InvalidOperationException(response.DebugInformation);
        }

        private static IConnectionSettingsValues CreateConnection(string connectionUrl)
        {
            return new ConnectionSettings
            (
                new Uri(connectionUrl)
            );
        }


    }
}
