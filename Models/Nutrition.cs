using MongoDB.Bson.Serialization.Attributes;

public class Nutrition {
    [BsonElement("calories")]
    public int Calories { get; set; }

    [BsonElement("protein")]
    public int Protein { get; set; }

    [BsonElement("fat")]
    public int Fat { get; set; }

    [BsonElement("carbs")]
    public int Carbs { get; set; }
}