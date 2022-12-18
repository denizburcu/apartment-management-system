using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CreditCardService.Models
{
    public class BaseDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
