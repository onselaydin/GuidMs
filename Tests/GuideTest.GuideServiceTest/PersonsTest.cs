using GuideService.Guide.Controllers;
using GuideService.Guide.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GuideTest.GuideServiceTest
{
    public class PersonsTest
    {
       

        [Fact]
        public async Task GetAllTest()
        {
            var client = new TestClientProvider().Client;
            var okResult = await client.GetAsync("persons");
            okResult.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, okResult.StatusCode);
      
        }
    }
}
