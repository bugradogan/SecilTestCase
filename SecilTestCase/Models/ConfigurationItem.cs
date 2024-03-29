using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ConfigurationItem
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonIgnore]
    public ObjectId _id;
    [BsonElement("Id")]
    public int Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Type")]
    public string Type { get; set; }

    [BsonElement("Value")]
    public string Value { get; set; }

    [BsonElement("IsActive")]
    public int IsActive { get; set; }

    [BsonElement("ApplicationName")]
    public string ApplicationName { get; set; }
  
}
