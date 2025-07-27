using AutoMapper;
using Business.Dtos.Requests.Application;
using Business.Dtos.Responses.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Business.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<CreateApplicationRequest, Application>();
            CreateMap<UpdateApplicationRequest, Application>();
            CreateMap<Application, GetApplicationResponse>();
        }
    }
}
