using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly_Web_Application.Models;
using Vidly_Web_Application.DTOs;

namespace Vidly_Web_Application.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Staff, StaffDTO>();
            Mapper.CreateMap<StaffDTO, Staff>();
        }
    }
}