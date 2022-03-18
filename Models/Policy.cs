using MongoDB.Bson.Serialization.Attributes;

namespace DgcPolicyApi.Models
{
    public class Policy
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string PolicyName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}