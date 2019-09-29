using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Performance.Samples.Data
{
    public class SamplesData
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Samples> collection;

        public SamplesData()
        {
            client = new MongoClient("mongodb+srv://admin:admin@cluster0-f7uk1.mongodb.net/test?retryWrites=true");
            database = client.GetDatabase("PerformanceSample");
            collection = database.GetCollection<Samples>("Samples");
        }


        public async Task<Samples> InsertAsync(Samples entity)
        {
            await collection.InsertOneAsync(entity);

            return entity;
        }

        public async Task<List<Samples>> GetAllAsync()
        {
            return (await collection.FindAsync(new BsonDocument())).ToList();
        }

    }

    public class Samples
    {
        public ObjectId Id { get; set; }
        public string html { get; set; }
    }
}
