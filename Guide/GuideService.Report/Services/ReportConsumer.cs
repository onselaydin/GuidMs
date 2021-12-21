using Guide.Shared.Messages;
using GuideService.Report.Models;
using GuideService.Report.Settings;
using MassTransit;
using MongoDB.Driver;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ClosedXML.Excel;

namespace GuideService.Report.Services
{
    public class ReportConsumer:IConsumer<ReportRequestEvent>
    {
        private readonly IMongoCollection<Communication> _communicationCollection;
        public ReportConsumer(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _communicationCollection = database.GetCollection<Communication>(databaseSettings.CommunicationCollectionName);

        }
        public async Task Consume(ConsumeContext<ReportRequestEvent> context)
        {
            var receiveMessage = context;

            var results = await _communicationCollection.Aggregate<Communication>()
              .Match(e => e.Type == "Konum")
               .Group(e => e.Content,
                g => new {
                    Location = g.Key,
                    LocationContactCount = g.Count()
                    //LocationPhoneCount=g.Count()
                }
                ).As<ReportModel>().ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sample Sheet");
                worksheet.Cell(1,1).SetValue("Konum");
                worksheet.Cell(1,2).SetValue("Konumdaki Kişi Sayısı");
                worksheet.Cell(2, 1).InsertData(results);
                workbook.SaveAs("Report.xlsx");
            }

        }

    }
}
