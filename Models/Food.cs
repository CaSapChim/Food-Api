using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class Food {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("foodName")]
    public string? FoodName { get; set; }

    [BsonElement("imageUrl")]
    public string ImageUrl { get; set; } = null!;

    [BsonElement("instruction")]
    public string Instruction { get; set; } = null!;

    [BsonElement("ingredient")]
    public List<Ingredient>? Ingredients { get; set; }

    [BsonElement("nutritions")]
    public Nutrition Nutrition { get; set; } = null!;
}