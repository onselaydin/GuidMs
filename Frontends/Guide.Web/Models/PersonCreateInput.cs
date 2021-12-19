using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Guide.Web.Models
{
    public class PersonCreateInput
    {
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Display(Name = "Soyad")]
        public string Surname { get; set; }

        [Display(Name = "Firma")]
        public string Company { get; set; }
    }
}
