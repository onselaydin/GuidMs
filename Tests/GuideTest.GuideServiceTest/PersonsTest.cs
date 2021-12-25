using GuideService.Guide.Controllers;
using GuideService.Guide.Dtos;
using GuideService.Guide.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GuideTest.GuideServiceTest
{
    public class PersonsTest
    {
        private readonly Mock<IPersonService> _mockRepo;
        private readonly PersonsController _controller;


        public PersonsTest()
        {
            _mockRepo = new Mock<IPersonService>();
            _controller = new PersonsController(_mockRepo.Object,null);

        }

        public List<PersonDto> GetAllPerson()
        {
            List<PersonDto> persons = new List<PersonDto>()
            {
                new PersonDto{ UUID="61bf2aa70b7520f907e71820", Name="Önsel", Surname="Aydın",Company="Rise tech."},
                 new PersonDto{ UUID="61bf3ecf3f61e4d963d073a8", Name="Yagmur", Surname="Aydın",Company="İnönü okulu"}
            };
            return persons;
        }
        public List<PersonUpdateDto> GetAllPersonsForUpdateDto()
        {
            List<PersonUpdateDto> persons = new List<PersonUpdateDto>()
            {
                new PersonUpdateDto{ UUID="61bf2aa70b7520f907e71820", Name="Önsel", Surname="Aydın",Company="Rise tech."},
                new PersonUpdateDto{ UUID="61bf3ecf3f61e4d963d073a8", Name="Yagmur", Surname="Aydın",Company="İnönü okulu"}

            };
            return persons;
        }
        public List<PersonCreateDto> GetAllPersonsForCreateDto()
        {
            List<PersonCreateDto> persons = new List<PersonCreateDto>()
            {
                new PersonCreateDto{  Name="Önsel", Surname="Aydın",Company="Rise tech."},
                new PersonCreateDto{  Name="Yagmur", Surname="Aydın",Company="İnönü okulu"}

            };
            return persons;
        }

        [Fact]
        public async void GetAll_ActionExecutes_ReturnsViewForIndex()
        {
            var result = await _controller.GetAll();
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async void GetPersons_ActionExecutes_ReturnOkResultWithPersons()
        {
            var persons = GetAllPerson();
            _mockRepo.Setup(p => p.GetAllAsync()).ReturnsAsync(persons);
            var result = await _controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnPerson = Assert.IsAssignableFrom<List<PersonDto>>(okResult.Value);
            Assert.Equal<int>(2,returnPerson.Count);

        }

        [Theory]
        [InlineData("61c09f14085046e0bf400000")]
        public async void GetPerson_IdInValid_ReturnNotFound(string id)
        {
            PersonDto person = null;
            _mockRepo.Setup(p => p.GetByIdAsync(id)).ReturnsAsync(person);
            var result = await _controller.GetById(id);

            Assert.IsType<NotFoundResult>(result);

        }

        [Theory]
        [InlineData("61bf2aa70b7520f907e71820")]
        [InlineData("61bf3ecf3f61e4d963d073a8")]
        public async void GetPerson_IdValid_ReturnOkResult(string id)
        {
            var persons = GetAllPerson();
            var person = persons.First(x => x.UUID == id);

            _mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(person);

            var result = await _controller.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnPerson = Assert.IsType<PersonDto>(okResult.Value);

            Assert.Equal(id, returnPerson.UUID);
            Assert.Equal(person.Name, returnPerson.Name);
        }

        [Fact]
        public async void UpdatePerson_IdIsNotEqualProduct_ReturnBadRequestResult()
        {
            var persons = GetAllPersonsForUpdateDto();

            var result = await _controller.Update(persons.First());

            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData("61bf2aa70b7520f907e71820")]
        public async void UpdateProduct_ActionExecutes_ReturnOkResult(string id)
        {
            var persons = GetAllPersonsForUpdateDto();
            var person = persons.First(x => x.UUID == id);

            _mockRepo.Setup(x => x.UpdateAsync(person)).ReturnsAsync(person);

            var result = await _controller.Update(person);

            _mockRepo.Verify(x => x.UpdateAsync(person), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void CreatePerson_ActionExecutes_ReturnCreatedAtAction()
        {
            var persons = GetAllPersonsForCreateDto();
            var person = persons.First();

            _mockRepo.Setup(x => x.CreateAsync(person));

            var result = await _controller.Create(person);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            _mockRepo.Verify(x => x.CreateAsync(person), Times.Once);

           
        }

        [Theory]
        [InlineData("61c09f14085046e0bf400000")]
        public async void DeletePerson_IdInValid_ReturnNotFound(string id)
        {

            _mockRepo.Setup(x => x.GetByIdAsync(id));

            var result = await _controller.Delete(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData("61bf2aa70b7520f907e71820")]
        public async void DeletePerson_ActionExecute_ReturnOkResult(string id)
        {
            var persons = GetAllPerson();
            var person = persons.First(x => x.UUID == id);
            _mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(person);
            _mockRepo.Setup(x => x.DeleteAsync(person.UUID));

            var okResult = await _controller.Delete(id);

            _mockRepo.Verify(x => x.DeleteAsync(person.UUID), Times.Once);

            Assert.IsType<NotFoundResult>(okResult);
        }
    }
}
