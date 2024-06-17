using FoodAPI.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FoodAPI.Services.Database;

public class MongoDatabase
{
    private static MongoClient mongoClient = null!;
    private static IMongoDatabase mongoDatabase = null!;
    public MongoDatabase(IOptions<DatabaseSettings> databaseSettings)
    {
        mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
    } 

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return mongoDatabase.GetCollection<T>(collectionName);
    }

}