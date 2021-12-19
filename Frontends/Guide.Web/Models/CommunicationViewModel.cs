using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guide.Web.Models
{
    public class CommunicationViewModel
    {
        public string UUID { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string PersonId { get; set; }
    }
}
