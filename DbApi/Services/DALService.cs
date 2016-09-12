using System.Collections.Generic;
using ChatDAL.CommonModels;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace DbApi.Services
{
    public class DALService
    {
        //C:\Program Files\MongoDB\Server\3.2\bin
        //http://mongodb.github.io/mongo-csharp-driver/2.2/getting_started/quick_tour/

        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<BsonDocument> _collection;
        private string _dbName = "chat";
        private string _collectionName = "chatCollection";

        public DALService()
        {
            MongoConnection();
        }

        public string Get(int id)
        {
            var document = _collection.Find(new BsonDocument()).FirstOrDefault();
            
            return document.ToString();
        }

        public string Get()
        {
            var document = GetDocumentString();
            return document;
        }

        public void Create(List<ChatLog> chats)
        {
            var chatsBson = ToBsonArray(chats);
            var document = new BsonDocument
            {
                {"name", "chatLog"},
                {
                    "chats", chatsBson
                }
            };

            _collection.InsertOne(document);
        }

        public string Delete(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var deleteResult =  _collection.DeleteOne(filter);
            return deleteResult.DeletedCount.ToString();
        }

        public string Delete()
        {
            var filter = new BsonDocument();
            var deleteResult = _collection.DeleteMany(filter);
            return deleteResult.DeletedCount.ToString();
        }

        private void MongoConnection()
        {
            // To directly connect to a single MongoDB server
            // (this will not auto-discover the primary even if it's a member of a replica set)
            _client = new MongoClient();
            _database = _client.GetDatabase(_dbName);
            _collection = _database.GetCollection<BsonDocument>(_collectionName);

            //// or use a connection string
            //var client = new MongoClient("mongodb://localhost:27017");

            //// or, to connect to a replica set, with auto-discovery of the primary, supply a seed list of members
            //var client = new MongoClient("mongodb://localhost:27017,localhost:27018,localhost:27019");
        }

        private static BsonArray ToBsonArray<T>(List<T> chats)
        {
            var chatsJson = JsonConvert.SerializeObject(chats);
            return MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonArray>(chatsJson);
        }

        private string GetDocumentString()
        {
            //this should be paged as it returns ALL contents of collection
            var document = _collection.Find(new BsonDocument()).ToList();
            return document.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict });
        }
    }
}