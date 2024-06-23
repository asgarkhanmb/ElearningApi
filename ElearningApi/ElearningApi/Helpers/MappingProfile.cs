using AutoMapper;
using ElearningApi.DTOs.Abouts;
using ElearningApi.DTOs.Informations;
using ElearningApi.DTOs.Sliders;
using ElearningApi.Models;

namespace ElearningApi.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<SliderCreateDto, Slider>();
            CreateMap<Slider, SliderDto>();
            CreateMap<SliderEditDto, Slider>().ForMember(dest => dest.Image, opt => opt.Condition(src => (src.Image is not null)));

            CreateMap<AboutCreateDto, About>();
            CreateMap<About, AboutDto>();
            CreateMap<AboutEditDto, About>().ForMember(dest => dest.Image, opt => opt.Condition(src => (src.Image is not null)));

            CreateMap<InformationCreateDto, Information>();
            CreateMap<Information, InformationDto>();
            CreateMap<InformationEditDto, Information>().ForMember(dest => dest.Icon, opt => opt.Condition(src => (src.Icon is not null)));


        }
    }
}
