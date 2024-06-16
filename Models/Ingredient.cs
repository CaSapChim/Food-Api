using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoodAPI.Models;

public class Ingredient {
    [BsonElement("name")]
    public string Name { get; set; } = null!;
    
    [BsonElement("quantity")]
    public string Quantity {get; set; } = null!; 
}