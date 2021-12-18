using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GuideService.Guide.Models
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CommunicationId { get; set; }

        [BsonIgnore]
        public Communication Communication { get; set; }
    }
}
