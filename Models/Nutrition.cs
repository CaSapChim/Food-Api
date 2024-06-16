using MongoDB.Bson.Serialization.Attributes;

namespace FoodAPI.Models;
public class Nutrition
{
    [BsonElement("calories")]
    public int Calories { get; set; }

    [BsonElement("protein")]
    public int Protein { get; set; }

    [BsonElement("fat")]
    public int Fat { get; set; }

    [BsonElement("carbs")]
    public int Carbs { get; set; }
}