using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guide.Web.Models
{
    public class PersonViewModel
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public IList<CommunicationViewModel> Communications { get; set; }
    }
}
