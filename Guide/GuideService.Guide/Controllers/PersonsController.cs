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
    public class PersonsController : CustomBaseController
    {
        private readonly IPersonService _personService;
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
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
