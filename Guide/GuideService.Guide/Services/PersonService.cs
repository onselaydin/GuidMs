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

namespace GuideService.Guide.Services
{
    public class PersonService: IPersonService
    {
        private readonly IMongoCollection<Person> _personCollection;
        private readonly IMongoCollection<Communication> _communicationCollection;
        private readonly IMapper _mapper;
        private readonly Mass.IPublishEndpoint _publishEndpoint;

        public PersonService(IMapper mapper, IDatabaseSettings databaseSettings, Mass.IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _personCollection = database.GetCollection<Person>(databaseSettings.PersonCollectionName);
            _communicationCollection = database.GetCollection<Communication>(databaseSettings.CommunicationCollectionName);
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Response<PersonDto>> CreateAsync(PersonCreateDto personCreateDto)
        {
            var newPerson = _mapper.Map<Person>(personCreateDto);

            await _personCollection.InsertOneAsync(newPerson);

            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(newPerson), 200);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _personCollection.DeleteOneAsync(x => x.UUID == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Person not found", 404);
            }
        }

        public async Task<Response<List<PersonDto>>> GetAllAsync()
        {
            var persons = await _personCollection.Find(person => true).ToListAsync();
            if (persons.Any())
            {
                foreach (var person in persons)
                {
                    person.Communication = await _communicationCollection.Find<Communication>(x => x.UUID == person.CommunicationId).FirstAsync();
                }
            }
            else
            {
                persons = new List<Person>();
            }
            return Response<List<PersonDto>>.Success(_mapper.Map<List<PersonDto>>(persons), 200);
        }

        public async Task<Response<List<PersonDto>>> GetAllByCommunicationIdAsync(string communicationId)
        {
            var persons = await _personCollection.Find<Person>(x => x.CommunicationId == communicationId).ToListAsync();

            if (persons.Any())
            {
                foreach (var person in persons)
                {
                    person.Communication = await _communicationCollection.Find<Communication>(x => x.UUID == person.CommunicationId).FirstAsync();
                }
            }
            else
            {
                persons = new List<Person>();
            }

            return Response<List<PersonDto>>.Success(_mapper.Map<List<PersonDto>>(persons), 200);
        }

        public async Task<Response<PersonDto>> GetByIdAsync(string id)
        {
            var person = await _personCollection.Find<Person>(x => x.UUID == id).FirstOrDefaultAsync();
            if (person == null)
            {
                return Response<PersonDto>.Fail("Person not found", 404);
            }
            person.Communication = await _communicationCollection.Find<Communication>(x => x.UUID == person.CommunicationId).FirstAsync();
            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(person), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(PersonUpdateDto personUpdateDto)
        {
            var updatePerson = _mapper.Map<Person>(personUpdateDto);

            var result = await _personCollection.FindOneAndReplaceAsync(x => x.UUID == personUpdateDto.UUID, updatePerson);

            if (result == null)
            {
                return Response<NoContent>.Fail("Person not found", 404);
            }

            return Response<NoContent>.Success(204);
        }
    }
}
