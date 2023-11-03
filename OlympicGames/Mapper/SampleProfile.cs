using AutoMapper;

namespace OlympicGames.Mapper
{
    public class SampleProfile : Profile
    {
        public SampleProfile()
        {
            CreateMap<Sample, Sample>();
        }
    }
}
