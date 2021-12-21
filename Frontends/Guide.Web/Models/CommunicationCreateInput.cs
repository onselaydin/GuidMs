using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Guide.Web.Models
{
    public class CommunicationCreateInput
    {
        [Display(Name = "Bilgi Tipi")]
        public string Type { get; set; }
        [Display(Name = "Bilgi İçeriği")]
        public string Content { get; set; }
        public string PersonId { get; set; }
    }
}
