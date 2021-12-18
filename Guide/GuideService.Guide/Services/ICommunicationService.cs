﻿using Guide.Shared.Dtos;
using GuideService.Guide.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuideService.Guide.Services
{
    public interface ICommunicationService
    {
        Task<Response<List<CommunicationDto>>> GetAllAsync();
        Task<Response<CommunicationDto>> CreateAsync(CommunicationDto person);
        Task<Response<CommunicationDto>> GetByIdAsync(string id);
    }
}
