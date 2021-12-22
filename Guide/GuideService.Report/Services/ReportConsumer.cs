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
using System;
using Microsoft.Extensions.Logging;

namespace GuideService.Report.Services
{
    public class ReportConsumer:IConsumer<ReportRequestEvent>
    {
        private readonly IMongoCollection<ReportRequestEvent> _reportRequestCollection;


        private readonly IMongoCollection<Communication> _communicationCollection;
        private readonly ILogger _logger;
        public ReportConsumer(IDatabaseSettings databaseSettings, ILogger<ReportRequestEvent> logger)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _communicationCollection = database.GetCollection<Communication>(databaseSettings.CommunicationCollectionName);
            _reportRequestCollection = database.GetCollection<ReportRequestEvent>(databaseSettings.ReportRequestCollectionName);
            this._logger = logger;

        }
        public async Task Consume(ConsumeContext<ReportRequestEvent> context)
        {
            var receiveMessage = context;
            context.Message.Status = true;
            var createReportMessageCommand = new ReportRequestEvent();
            createReportMessageCommand.UUID = context.Message.UUID;
            createReportMessageCommand.RequestTime = context.Message.RequestTime;
            createReportMessageCommand.Status = true;

            //await _reportRequestCollection.InsertOneAsync(context.Message);
            var result = await _reportRequestCollection.FindOneAndReplaceAsync(x => x.UUID == context.Message.UUID, createReportMessageCommand);

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
                workbook.SaveAs(context.Message.RequestTime.ToString("yyyyMMddHHmm") +"Report.xlsx");
            }
            _logger.LogInformation("Rapor dosyası oluştu");
        }

    }
}
