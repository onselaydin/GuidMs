using FreeCourse.Shared.ControllerBases;
using GuideService.Guide.Dtos;
using GuideService.Guide.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideService.Guide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationsController : CustomBaseController
    {
        private readonly ICommunicationService _communicationService;

        public CommunicationsController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comms = await _communicationService.GetAllAsync();
            return CreateActionResultInstance(comms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category = await _communicationService.GetByIdAsync(id);

            return CreateActionResultInstance(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommunicationCreateDto communicationCreateDto)
        {
            var response = await _communicationService.CreateAsync(communicationCreateDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _communicationService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }

    }
}
