
using Guide.Shared.Dtos;
using Guide.Shared.Messages;
using GuideService.Guide.Dtos;
using GuideService.Guide.Services;
using Mass=MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
//using Guide.Shared.ControllerBases;

namespace GuideService.Guide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase//CustomBaseController
    {
        private readonly Mass.ISendEndpointProvider _sendEndpointProvider;

        private readonly IPersonService _personService;
        public PersonsController(IPersonService personService, Mass.ISendEndpointProvider sendEndpointProvider)
        {
            _personService = personService;
            this._sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        [Route("RequestReport")]
        public async Task<IActionResult> RequestReport()
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-report-request"));
            var createReportMessageCommand = new ReportRequestEvent();
            //createReportMessageCommand.UUID = Guid.NewGuid();
            createReportMessageCommand.RequestTime = DateTime.Now;
            createReportMessageCommand.Status = false;
            var response = await _personService.CreateReportRequest(createReportMessageCommand);
            
            await sendEndpoint.Send(createReportMessageCommand);
     
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personService.GetAllAsync();
            //return CreateActionResultInstance(persons);
            return Ok(persons);
        }

        [HttpGet]
        [Route("GetReportsAsync")]
        public async Task<IActionResult> GetReportsAsync()
        {
            var reports = await _personService.GetAllReportAsync();
            if (reports == null)
            {
                return NotFound();
            }
            //return CreateActionResultInstance(reports);
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _personService.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            //return CreateActionResultInstance(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonCreateDto personCreateDto)
        {
            await _personService.CreateAsync(personCreateDto);
            return CreatedAtAction("GetAll", new { id = personCreateDto.Surname}, personCreateDto);        }

        [HttpPut]
        public async Task<IActionResult> Update(PersonUpdateDto personUpdateDto)
        {
            var response = await _personService.UpdateAsync(personUpdateDto);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _personService.DeleteAsync(id);
            if(response.Equals(0))
            {
                return NotFound();
            }
            return Ok();
        }


    }
}
