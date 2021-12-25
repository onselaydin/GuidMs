using AutoMapper;
using GuideService.Guide.Dtos;
using GuideService.Guide.Models;
using GuideService.Guide.Settings;
using Mass=MassTransit;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guide.Shared.Dtos;
using Guide.Shared.Messages;

namespace GuideService.Guide.Services
{
    public class PersonService: IPersonService
    {
        private readonly IMongoCollection<ReportRequestEvent> _reportRequestCollection;
        private readonly IMongoCollection<Person> _personCollection;
        private readonly IMongoCollection<Communication> _communicationCollection;
        private readonly IMapper _mapper;
      

        public PersonService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _reportRequestCollection = database.GetCollection<ReportRequestEvent>(databaseSettings.ReportRequestCollectionName);
            _personCollection = database.GetCollection<Person>(databaseSettings.PersonCollectionName);
            _communicationCollection = database.GetCollection<Communication>(databaseSettings.CommunicationCollectionName);
            _mapper = mapper;
        }

        public async Task<ReportRequestEvent> CreateReportRequest(ReportRequestEvent reportRequestEvent)
        {
             await _reportRequestCollection.InsertOneAsync(reportRequestEvent);
            //return Response<ReportRequestEvent>.Success(reportRequestEvent, 200);
            return reportRequestEvent;
        }

        public async Task<List<ReportRequestEvent>> GetAllReportAsync()
        {
            var reports = await _reportRequestCollection.Find(report => true).ToListAsync();


            //return Response<List<ReportRequestEvent>>.Success(_mapper.Map<List<ReportRequestEvent>>(reports), 200);
            return reports;
        }

  
        public async Task CreateAsync(PersonCreateDto personCreateDto)
        {
            var newPerson = _mapper.Map<Person>(personCreateDto);

            await _personCollection.InsertOneAsync(newPerson);
             _mapper.Map<PersonCreateDto>(newPerson);
            //return maping;
        }

        public async Task<long> DeleteAsync(string id)
        {
            var result = await _personCollection.DeleteOneAsync(x => x.UUID == id);

            if (result.DeletedCount > 0)
            {
                return result.DeletedCount;
            }
            else
            {
                return 404;
            }
        }

        public async Task<List<PersonDto>> GetAllAsync()
        {
            var persons = await _personCollection.Find(person => true).ToListAsync();
            if (persons.Any())
            {
                foreach (var person in persons)
                {
                    person.Communications = await _communicationCollection.Find<Communication>(x => x.PersonId == person.UUID).ToListAsync();
                }
            }
            else
            {
                persons = new List<Person>();
            }

            //return Response<List<PersonDto>>.Success(_mapper.Map<List<PersonDto>>(persons), 200);
            return _mapper.Map<List<PersonDto>>(persons);
        }


        public async Task<PersonDto> GetByIdAsync(string id)
        {
            var person = await _personCollection.Find<Person>(x => x.UUID == id).FirstOrDefaultAsync();
            if (person == null)
            {
                return null;
            }
            else
            {
                person.Communications = await _communicationCollection.Find<Communication>(x => x.PersonId == person.UUID).ToListAsync();
            }

            return _mapper.Map<PersonDto>(person);


        }

        public async Task<PersonUpdateDto> UpdateAsync(PersonUpdateDto personUpdateDto)
        {
            var updatePerson = _mapper.Map<Person>(personUpdateDto);

            var result = await _personCollection.FindOneAndReplaceAsync(x => x.UUID == personUpdateDto.UUID, updatePerson);

            if (result == null)
            {
                return personUpdateDto;
            }

            return personUpdateDto;
        }



    }
}
