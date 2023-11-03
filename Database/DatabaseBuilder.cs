using System;
using Microsoft.Azure.Cosmos;

namespace HPW.Database
{
    public class DatabaseBuilder : IDisposable
    {
        private CosmosClient _client;
        private DatabaseSettings _settings;
        public DatabaseBuilder(IDatabaseConfiguration config)
        {
            _settings = config.Settings;

            _client = new CosmosClient(_settings.ConnectionString);
        }

        public Microsoft.Azure.Cosmos.Database Build()
        {
            return _client.GetDatabase(_settings.DatabaseName);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            _client.Dispose();
        }
    }
}