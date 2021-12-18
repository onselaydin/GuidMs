using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideService.Guide.Dtos
{
    public class PersonCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CommunicationId { get; set; }

    }
}
