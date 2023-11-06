namespace HPW.Database
{
    public class CosmosConfiguration : IDatabaseConfiguration
    {
        public DatabaseSettings Settings
        {
            get
            {
                return new DatabaseSettings()
                {
                    //TODO:Extract these to settings.json
                    ConnectionString = "AccountEndpoint=https://hpw-database.documents.azure.com:443/;AccountKey=5B7ExvcnVrGUzTkr0bOG54TBshG6GUgYmvKosSizRaNKTg6as6c0l0DqQuzV1FQI7RD3YcFIbpCvACDbkPCZvw==;",
                    DatabaseName = "hpw-database"
                };
            }
        }
    }
}
