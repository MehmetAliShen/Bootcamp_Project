using AutoMapper;
using Business.Dtos.Requests.Bootcamp;
using Business.Dtos.Responses.Bootcamp;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class BootcampProfile : Profile
    {
        public BootcampProfile()
        {
            CreateMap<CreateBootcampRequest, Bootcamp>();
            CreateMap<UpdateBootcampRequest, Bootcamp>();
            CreateMap<Bootcamp, GetBootcampResponse>();
        }
    }
}
