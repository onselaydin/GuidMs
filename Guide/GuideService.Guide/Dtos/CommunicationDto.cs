using GuideService.Guide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideService.Guide.Dtos
{
    public class CommunicationDto
    {
        public string UUID { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string PersonId { get; set; }
        public Person Person { get; set; }
    }
}
