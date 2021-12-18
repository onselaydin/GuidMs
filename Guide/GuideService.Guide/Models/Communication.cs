﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace GuideService.Guide.Models
{
    public class Communication
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UUID { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }

       
    }
}
