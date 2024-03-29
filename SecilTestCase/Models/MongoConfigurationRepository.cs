using MongoDB.Bson;
using MongoDB.Driver;
using SecilTestCase.Models.Repository;


public class MongoConfigurationRepository : IConfigurationRepository
{
    private readonly IMongoCollection<ConfigurationItem> _collection;

    public MongoConfigurationRepository(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("ConfigDB");
        _collection = database.GetCollection<ConfigurationItem>("SecilStore");
    }

    public List<ConfigurationItem> GetConfigurations(string applicationName)
    {
        return _collection.Find(c => c.ApplicationName == applicationName && c.IsActive == 1).ToList();
    }

    public List<ConfigurationItem> GetAllConfigurations()
    {
        return _collection.Find(Builders<ConfigurationItem>.Filter.Empty).ToList();
    }


    public void UpdateConfiguration(ConfigurationItem item)
    {
        var filter = Builders<ConfigurationItem>.Filter.Eq(c => c.Id, item.Id);
        var update = Builders<ConfigurationItem>.Update.Set(c => c.Value, item.Value);
        _collection.UpdateOne(filter, update);
    }
}
