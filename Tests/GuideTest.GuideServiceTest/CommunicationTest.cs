using Guide.Shared.Dtos;
using GuideService.Guide.Controllers;
using GuideService.Guide.Dtos;
using GuideService.Guide.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GuideTest.GuideServiceTest
{
    
    public class CommunicationTest
    {
        private readonly Mock<ICommunicationService> _mockRepo;
        private readonly CommunicationsController _controller;
        private readonly IMapper _mapper;
        public CommunicationTest(IMapper mapper)
        {
            _mockRepo = new Mock<ICommunicationService>();
            _controller = new CommunicationsController(_mockRepo.Object);
            _mapper = new Mapper(mapper.ConfigurationProvider);
        }


        public Response<List<CommunicationDto>> GetAllPerson()
        {
            List<CommunicationDto> communications = new List<CommunicationDto>()
            {
                new CommunicationDto{ UUID="61bf314f5c7139f7bd556e98", Type="Telefon Numarası", Content="05434353077",PersonId="61bf2aa70b7520f907e71820"},
                  new CommunicationDto{ UUID="61bf31a05c7139f7bd556e99", Type="E-mail Adresi", Content="onselaydin@gmail.com",PersonId="61bf2aa70b7520f907e71820"}
            };
            //return persons;
            return Response<List<CommunicationDto>>.Success(_mapper.Map<List<CommunicationDto>>(communications), 200);
        }

        [Fact]
        public async void GetContacts_ActionExecutes_ReturnOkResult()
        {
            var persons = GetAllPerson();
            _mockRepo.Setup(p => p.GetAllAsync());
            var result = await _controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnPerson = Assert.IsAssignableFrom<List<PersonDto>>(okResult.Value);
            Assert.Equal<int>(2, returnPerson.Count);

        }

    }
}
