using AutoMapper;
using Business.Dtos.Requests.Blacklist;
using Business.Dtos.Responses.Blacklist;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class BlacklistProfile : Profile
    {
        public BlacklistProfile()
        {
            CreateMap<CreateBlacklistRequest, Blacklist>();
            CreateMap<UpdateBlacklistRequest, Blacklist>();
            CreateMap<Blacklist, GetBlacklistResponse>();
        }
    }

}
