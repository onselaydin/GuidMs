using AutoMapper;
using GuideService.Guide.Dtos;
using GuideService.Guide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideService.Guide.Mapping
{
    public class GenaralMapping:Profile
    {
        public GenaralMapping()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Person, PersonCreateDto>().ReverseMap();
            CreateMap<Person, PersonUpdateDto>().ReverseMap();

            CreateMap<Communication, CommunicationDto>().ReverseMap();
        }
    }
}
