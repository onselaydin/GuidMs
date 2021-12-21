using GuideService.Guide;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace GuideTest.GuideServiceTest
{

    public class TestClientProvider
    {
        public HttpClient Client { get; set; }

        public TestClientProvider()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            server.BaseAddress = new System.Uri("http://localhost:5000/api/");
            Client = server.CreateClient();
        }
    }
}
