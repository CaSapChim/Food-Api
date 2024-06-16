namespace FoodAPI.Config;
public class DatabaseSettings {
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string FoodCollectionName { get; set; } = null!;
}