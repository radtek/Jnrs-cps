using MongoDB.Bson;
using MongoDB.Driver;

namespace Learun.DataBase.Mongo
{
    public class MongoDBAggregate
    {
        //连接字符串
        string ConnString;
        public MongoDBAggregate(string vConnString)
        {
            ConnString = vConnString;
        }


        public IMongoCollection<BsonDocument> GetCollection(string dbaseName, string collectionNmae)
        {
            var server = new MongoClient(ConnString);
            return server.GetDatabase(dbaseName).GetCollection<BsonDocument>(collectionNmae);
        }


        public IAsyncCursor<BsonDocument> GetAggregate(string dbName, string collectionName, PipelineDefinition<BsonDocument, BsonDocument> pipeline)
        {
            return GetCollection(dbName, collectionName).Aggregate(pipeline);
        }
    }
}