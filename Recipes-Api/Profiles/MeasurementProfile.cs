using AutoMapper;
using Recipes_Api.Dto;

namespace Recipes_Api.Profiles
{
    public class MeasurementProfile : Profile
    {
        public MeasurementProfile()
        {
            CreateMap<Measurement, MeasurementDto>();
            CreateMap<MeasurementDto, Measurement>();
        }
    }
}
