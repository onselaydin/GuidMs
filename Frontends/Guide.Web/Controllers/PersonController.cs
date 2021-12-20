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
    }
}
