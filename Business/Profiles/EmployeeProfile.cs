using AutoMapper;
using Business.Dtos.Requests.Employee;
using Business.Dtos.Responses.Employee;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeRequest, Employee>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
            CreateMap<UpdateEmployeeRequest, Employee>();
            CreateMap<Employee, GetEmployeeResponse>();
        }
    }
}
