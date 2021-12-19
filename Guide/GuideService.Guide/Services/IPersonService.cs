using Guide.Shared.Dtos;
using GuideService.Guide.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuideService.Guide.Services
{
    public interface IPersonService
    {
        Task<Response<List<PersonDto>>> GetAllAsync();
        Task<Response<PersonDto>> GetByIdAsync(string id);
        Task<Response<PersonDto>> CreateAsync(PersonCreateDto personCreateDto);
        Task<Response<NoContent>> UpdateAsync(PersonUpdateDto personUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
