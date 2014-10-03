using System;

using AutoMapper;

using Company.Module.Domain;
using Company.Module.Domain.Interfaces;
using Company.Module.Shared.DTO;

namespace Company.Module.Web.Host
{
    public class ObjectMappingConfig
    {
        //// ----------------------------------------------------------------------------------------------------------

        public static void Configure()
        {
            Mapper.CreateMap<Patient, PatientDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => String.Format("{0} {1}", src.FirstName, src.Surname)));

            Mapper.CreateMap<PatientDTO, Patient>()
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());

            Mapper.CreateMap<TestResult, TestResultDTO>();

            Mapper.CreateMap<TestResultDTO, TestResult>()
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}