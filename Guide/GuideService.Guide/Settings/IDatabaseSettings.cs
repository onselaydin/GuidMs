using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideService.Guide.Settings
{
    public interface IDatabaseSettings
    {
        public string ReportRequestCollectionName { get; set; }
        public string PersonCollectionName { get; set; }
        public string CommunicationCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
