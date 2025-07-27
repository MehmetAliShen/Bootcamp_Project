using AutoMapper;
using Business.Dtos.Requests.Applicant;
using Business.Dtos.Responses.Applicant;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class ApplicantProfile : Profile
    {
        public ApplicantProfile()
        {
            CreateMap<CreateApplicantRequest, Applicant>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
            CreateMap<UpdateApplicantRequest, Applicant>();
            CreateMap<Applicant, GetApplicantResponse>();
        }
    }
}
