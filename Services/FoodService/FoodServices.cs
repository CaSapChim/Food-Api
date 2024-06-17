using FoodAPI.Config;
using FoodAPI.Models;
using FoodAPI.Services.Database;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FoodAPI.Services.FoodService;
public class FoodServices(IOptions<DatabaseSettings> databaseSettings, MongoDatabase mongoDatabase)
{
    private readonly IMongoCollection<FoodInDatabase> _foodCollection = mongoDatabase.GetCollection<FoodInDatabase>(databaseSettings.Value.FoodCollectionName);

    public async Task<List<FoodInDatabase>> GetFoods()
    {
        return await _foodCollection.Find(food => true).ToListAsync();
    }

    public async Task<FoodPost> CreateFood(FoodPost food)
    {
        var insertedFood = CreateInsertedFood(food);
        await _foodCollection.InsertOneAsync(insertedFood);
        return food;
    }

    public async Task<List<FoodInDatabase>> SearchFoods(string? ingredientName, string? foodName)
    {
        List<FilterDefinition<FoodInDatabase>> filters = ExtractFilters(ingredientName, foodName);

        var filter = filters.Count > 0 ? Builders<FoodInDatabase>.Filter.And(filters) : Builders<FoodInDatabase>.Filter.Empty;

        return await _foodCollection.Find(filter).ToListAsync();
    }

    private static List<FilterDefinition<FoodInDatabase>> ExtractFilters(string? ingredientName, string? foodName)
    {
        var filters = new List<FilterDefinition<FoodInDatabase>>();

        if (!string.IsNullOrEmpty(ingredientName))
        {
            filters.Add(Builders<FoodInDatabase>.Filter.ElemMatch(food => food.Ingredients,
                ingredient => ingredient.Name.Equals(ingredientName, StringComparison.CurrentCultureIgnoreCase)
            ));
        }

        if (!string.IsNullOrEmpty(foodName))
        {
            filters.Add(Builders<FoodInDatabase>.Filter.Regex("foodName", new BsonRegularExpression(foodName, "i")));
        }

        return filters;
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

    private static FoodInDatabase CreateInsertedFood(FoodPost food)
    {
        return new FoodInDatabase(
                    food.FoodName,
                    food.ImageUrl,
                    food.Instruction,
                    food.Ingredients,
                    food.Nutrition
                );
    }
}