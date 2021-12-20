using Guide.Web.Models;
using Guide.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guide.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _personService.GetAllPersonAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonCreateInput personCreateInput)
        {
         
            if (!ModelState.IsValid)
            {
                return View();
            }

          
            await _personService.CreatePersonAsync(personCreateInput);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Communication(string id)
        {
            var person = await _personService.GetByPersonId(id);
            if (person == null)
            {
                RedirectToAction(nameof(Index));
            }
            return View(person);
        }
        public async Task<IActionResult> Update(string id)
        {
            var person = await _personService.GetByPersonId(id);
            if (person == null)
            {
                RedirectToAction(nameof(Index));
            }
            PersonUpdateInput personUpdateInput = new()
            {
                UUID = person.UUID,
                Name = person.Name,
                Surname = person.Surname,
                Company = person.Company
            };

            return View(personUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PersonUpdateInput personUpdateInput)
        {

            var categories = await _personService.GetAllPersonAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _personService.UpdatePersonAsync(personUpdateInput);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(string id)
        {
            await _personService.DeletePersonAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
