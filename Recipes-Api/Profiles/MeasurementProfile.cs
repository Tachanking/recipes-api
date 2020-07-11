using AutoMapper;
using Recipes_API.Dto;

namespace Recipes_API.Profiles
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
