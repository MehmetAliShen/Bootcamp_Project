using AutoMapper;
using Business.Dtos.Requests.Instructor;
using Business.Dtos.Responses.Instructor;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            CreateMap<CreateInstructorRequest, Instructor>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
            CreateMap<UpdateInstructorRequest, Instructor>();
            CreateMap<Instructor, GetInstructorResponse>();
        }
    }
}
