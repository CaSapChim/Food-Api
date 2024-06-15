using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Ingredient {
    [BsonElement("name")]
    public string Name { get; set; } = null!;
    
    [BsonElement("quantity")]
    public string Quantity {get; set; } = null!; 
}