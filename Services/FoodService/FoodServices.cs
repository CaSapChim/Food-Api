using FoodAPI.Config;
using FoodAPI.Models;
using FoodAPI.Services.Database;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FoodAPI.Services.FoodService;
public class FoodServices(IOptions<DatabaseSettings> databaseSettings, MongoDatabase mongoDatabase)
{
    private readonly IMongoCollection<Food> _foodCollection = mongoDatabase.GetCollection<Food>(databaseSettings.Value.FoodCollectionName);

    public async Task<List<Food>> GetFoods()
    {
        return await _foodCollection.Find(food => true).ToListAsync();
    }

    public async Task<Food> CreateFood(Food food)
    {
        await _foodCollection.InsertOneAsync(food);
        return food;
    }

    public async Task<List<Food>> SearchFoods(string? ingredientName, string? foodName)
    {
        var filters = new List<FilterDefinition<Food>>();

        if (!string.IsNullOrEmpty(ingredientName))
        {
            filters.Add(Builders<Food>.Filter.ElemMatch(food => food.Ingredients,
                ingredient => ingredient.Name.Equals(ingredientName, StringComparison.CurrentCultureIgnoreCase)
            ));
        }

        if (!string.IsNullOrEmpty(foodName))
        {
            filters.Add(Builders<Food>.Filter.Regex("foodName", new BsonRegularExpression(foodName, "i")));
        }

        var filter = filters.Count > 0 ? Builders<Food>.Filter.And(filters) : Builders<Food>.Filter.Empty;

        return await _foodCollection.Find(filter).ToListAsync();
    }

    // public async Task<List<Food>> SearchByIngredient(string ingredientName)
    // {
    //     var filter = Builders<Food>.Filter.Where(food => food.Ingredients.Any(ingredient => ingredient.Name.ToLower() == ingredientName));
    //     return await _foodCollection.Find(filter).ToListAsync();
    // }

    // public async Task<List<Food>> SearchByFoodName(string foodName)
    // {
    //     var filter = Builders<Food>.Filter.Regex("foodName", new BsonRegularExpression(foodName, "i"));
    //     return await _foodCollection.Find(filter).ToListAsync();
    // }
}