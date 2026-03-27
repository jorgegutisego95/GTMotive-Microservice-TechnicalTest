using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    public class MongoService
    {
        private readonly MongoDbSettings _settings;

        static MongoService()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }

        public MongoService(IOptions<MongoDbSettings> options)
        {
            _settings = options.Value;
            MongoClient = new MongoClient(_settings.ConnectionString);
        }

        public MongoClient MongoClient { get; }

        public IMongoDatabase Database => MongoClient.GetDatabase(_settings.MongoDbDatabaseName);
    }
}
