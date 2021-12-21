using Guide.Shared.Messages;
using GuideService.Report.Models;
using Mass=MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guide.Shared.Dtos;

namespace GuideService.Report.Services
{
    public interface IReportService
    {
        Task<List<ReportModel>> GetAllAsync();
    }
}
