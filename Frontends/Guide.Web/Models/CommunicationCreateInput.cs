using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guide.Web.Models
{
    public class CommunicationCreateInput
    {
        public string Type { get; set; }
        public string Content { get; set; }
        public string PersonId { get; set; }
    }
}
