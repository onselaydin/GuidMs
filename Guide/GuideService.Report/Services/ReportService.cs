using GuideService.Report.Models;
using GuideService.Report.Settings;
using Mass=MassTransit;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guide.Shared.Messages;
using Guide.Shared.Dtos;
using MassTransit;

namespace GuideService.Report.Services
{
    public class ReportService : IReportService
    {
        private readonly IMongoCollection<Communication> _communicationCollection;
        public ReportService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _communicationCollection = database.GetCollection<Communication>(databaseSettings.CommunicationCollectionName);

        }

        
        public async Task<List<ReportModel>> GetAllAsync()
        {
            var result = await _communicationCollection.Aggregate<Communication>()
               .Match(e => e.Type == "Konum")
                .Group(e => e.Content, 
                    g => new {
                        Location = g.Key,
                        LocationContactCount = g.Count()
                        //LocationPhoneCount=g.Count()
                }
                    ).As<ReportModel>().ToListAsync();
            //return result.Cast<ReportModel>().ToList();
            return result;
        }
    }
}
