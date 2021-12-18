using AutoMapper;
using Guide.Shared.Dtos;
using GuideService.Guide.Dtos;
using GuideService.Guide.Models;
using GuideService.Guide.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuideService.Guide.Services
{
    public class CommunicationService:ICommunicationService
    {
        private readonly IMongoCollection<Communication> _communicationCollection;
        private readonly IMapper _mapper;

        public CommunicationService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _communicationCollection = database.GetCollection<Communication>(databaseSettings.CommunicationCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<CommunicationDto>> CreateAsync(CommunicationDto communicationDto)
        {
            var communication = _mapper.Map<Communication>(communicationDto);
            await _communicationCollection.InsertOneAsync(communication);
            return Response<CommunicationDto>.Success(_mapper.Map<CommunicationDto>(communication), 200);
        }

        public async Task<Response<List<CommunicationDto>>> GetAllAsync()
        {
            var communications = await _communicationCollection.Find(c => true).ToListAsync();
            return Response<List<CommunicationDto>>.Success(_mapper.Map<List<CommunicationDto>>(communications), 200);
        }

        public async Task<Response<CommunicationDto>> GetByIdAsync(string id)
        {
            var communication = await _communicationCollection.Find<Communication>(x => x.UUID == id).FirstOrDefaultAsync();
            if (communication == null)
            {
                return Response<CommunicationDto>.Fail("Communication not found", 404);
            }

            return Response<CommunicationDto>.Success(_mapper.Map<CommunicationDto>(communication), 200);
        }
    }
}
