using FreeCourse.Shared.ControllerBases;
using Guide.Shared.Dtos;
using Guide.Shared.Messages;
using GuideService.Guide.Dtos;
using GuideService.Guide.Services;
using Mass=MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GuideService.Guide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : CustomBaseController
    {
        private readonly Mass.ISendEndpointProvider _sendEndpointProvider;

        private readonly IPersonService _personService;
        public PersonsController(IPersonService personService, Mass.ISendEndpointProvider sendEndpointProvider)
        {
            _personService = personService;
            this._sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> RequestReport()
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-report-request"));
            var createReportMessageCommand = new ReportRequestEvent();
            //createReportMessageCommand.UUID = Guid.NewGuid();
            createReportMessageCommand.RequestTime = DateTime.Now;
            createReportMessageCommand.Status = false;
            var response = await _personService.CreateReportRequest(createReportMessageCommand);
            if(response.StatusCode==200)
            {
                await sendEndpoint.Send(createReportMessageCommand);
            }
           
            return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(200));
        }

       

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personService.GetAllAsync();
            return CreateActionResultInstance(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _personService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        

        [HttpPost]
        public async Task<IActionResult> Create(PersonCreateDto personCreateDto)
        {
            var response = await _personService.CreateAsync(personCreateDto);

            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PersonUpdateDto personUpdateDto)
        {
            var response = await _personService.UpdateAsync(personUpdateDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _personService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }


    }
}
