using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace FoodAPI.Models;
interface IFoodBase
{
    public string FoodName { get; set; }
    public string ImageUrl { get; set; }
    public string Instruction { get; set; }
    public List<Ingredient>? Ingredients { get; set; }
    public Nutrition Nutrition { get; set; }
}

public class FoodInDatabase(string foodName, string imageUrl, string instruction, List<Ingredient>? ingredients, Nutrition nutrition) : IFoodBase
{
    [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [BsonElement("foodName")]
    public string FoodName { get; set; } = foodName;

    [BsonElement("imageUrl")]
    public string ImageUrl { get; set; } = imageUrl;

    [BsonElement("instruction")]
    public string Instruction { get; set; } = instruction;
    [BsonElement("ingredient")]
    public List<Ingredient>? Ingredients { get; set; } = ingredients;

    [BsonElement("nutritions")]
    public Nutrition Nutrition { get; set; } = nutrition;
}

public class FoodPost : IFoodBase
{
    public string FoodName { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Instruction { get; set; } = null!;
    public List<Ingredient>? Ingredients { get; set; } = null!;
    public Nutrition Nutrition { get; set; }= null!;
}