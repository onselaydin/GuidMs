using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Shared.Messages
{
    public class ReportModel
    {
        [BsonElement("_id")]
        public string Location { get; set; }
        public int LocationContactCount { get; set; }
       // public int LocationPhoneCount { get; set; }
    }
}
