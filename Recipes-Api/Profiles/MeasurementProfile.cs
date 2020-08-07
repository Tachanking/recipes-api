using AutoMapper;
using Recipes_Api.Dto;
using Recipes_Api.Models;
using System.Linq;

namespace Recipes_Api.Profiles
{
    public class MeasurementProfile : Profile
    {
        public MeasurementProfile()
        {
            CreateMap<Measurement, MeasurementDto>().ForMember(dto => dto.Quantity, opt => opt.MapFrom(x => x.RecipeIngredientMeasurement.Select(y => y.Quantity).FirstOrDefault()));
            CreateMap<MeasurementDto, Measurement>();
        }
    }
}
