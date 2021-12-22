using Guide.Shared.Dtos;
using Guide.Shared.Messages;
using Guide.Web.Models;
using Guide.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Guide.Web.Services
{
    public class PersonService:IPersonService
    {
        private readonly HttpClient _client;

        public PersonService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<bool> CreateCommunicationAsync(CommunicationCreateInput communicationCreateInput)
        {
            var response = await _client.PostAsJsonAsync<CommunicationCreateInput>("communications", communicationCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreatePersonAsync(PersonCreateInput personCreateInput)
        {
            var response = await _client.PostAsJsonAsync<PersonCreateInput>("persons", personCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCommunicationAsync(string communicationId)
        {
            var response = await _client.DeleteAsync($"communications/{communicationId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePersonAsync(string personId)
        {
            var response = await _client.DeleteAsync($"persons/{personId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<PersonViewModel>> GetAllPersonAsync()
        {
            var response = await _client.GetAsync("persons");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseResult = await response.Content.ReadFromJsonAsync<Response<List<PersonViewModel>>>();
            return responseResult.Data;
        }

        public async Task<bool> RequestReport()
        {
            var response = await _client.GetAsync("persons/RequestReport");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return false;
        }
        public async Task<List<ReportRequestEvent>> GetAllReportAsync()
        {
            var response = await _client.GetAsync("persons/GetReportsAsync");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseResult = await response.Content.ReadFromJsonAsync<Response<List<ReportRequestEvent>>>();
            return responseResult.Data;
        }
        public async Task DownloadReport(string id)
        {
            await _client.GetAsync($"http://localhost:5002/api/report/{id}");
        }
        public async Task<PersonViewModel> GetByPersonId(string personId)
        {
            var response = await _client.GetAsync($"persons/{personId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseResult = await response.Content.ReadFromJsonAsync<Response<PersonViewModel>>();
            return responseResult.Data;
        }

        public async Task<bool> UpdatePersonAsync(PersonUpdateInput personUpdateInput)
        {
            var response = await _client.PutAsJsonAsync<PersonUpdateInput>("persons", personUpdateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
