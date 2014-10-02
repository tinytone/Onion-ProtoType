using AutoMapper;

using Company.Module.Domain;
using Company.Module.Shared.DTO;

namespace Company.Module.Web.Host
{
    public class DtoMapperConfig
    {
        //// ----------------------------------------------------------------------------------------------------------

        public static void CreateMaps()
        {
            Mapper.CreateMap<Patient, PatientDTO>();
            Mapper.CreateMap<PatientDTO, Patient>();
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}