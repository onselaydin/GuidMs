using Guide.Web.Models;
using Guide.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guide.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly ILogger _logger;
        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        #region person methods
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
        #endregion

        #region comminicaiton methods
        public async Task<IActionResult> CreateCommInfo(string id)
        {
            ViewBag.personId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommInfo(CommunicationCreateInput communicationCreateInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _personService.CreateCommunicationAsync(communicationCreateInput);
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
        public async Task<IActionResult> DeleteCommunication(string id)
        {
            await _personService.DeleteCommunicationAsync(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
        public async Task<IActionResult> Report()
        {
            return View();
        }
        public async Task<IActionResult> ReportRequest()
        {
            
            var result= await _personService.RequestReport();
            _logger.LogInformation(result.ToString());
            // return RedirectToAction(nameof(Report));
            return Ok();
        }
    }
}
