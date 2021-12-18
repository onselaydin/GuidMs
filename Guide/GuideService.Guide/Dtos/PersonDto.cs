using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideService.Guide.Dtos
{
    public class PersonDto
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CommunicationId { get; set; }
        public CommunicationDto Communication { get; set; }
    }
}
