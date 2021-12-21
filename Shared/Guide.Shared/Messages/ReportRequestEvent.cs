using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Shared.Messages
{
    public class ReportRequestEvent
    {
        public Guid UUID { get; set; }
        public DateTime RequestTime { get; set; }
        public bool Status { get; set; }
    }
}
