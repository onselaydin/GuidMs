using Guide.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guide.Web.Services.Interfaces
{
    public interface IPersonService
    {
        Task<List<PersonViewModel>> GetAllPersonAsync();
        Task<PersonViewModel> GetByPersonId(string personId);
        Task<bool> CreatePersonAsync(PersonCreateInput personCreateInput);
        Task<bool> UpdatePersonAsync(PersonUpdateInput personUpdateInput);
        Task<bool> DeletePersonAsync(string personId);
        Task<bool> CreateCommunicationAsync(CommunicationCreateInput communicationCreateInput);
        Task<bool> DeleteCommunicationAsync(string communicationId);
        Task<bool> RequestReport();
    }
}
