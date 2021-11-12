using AutoMapper;
using Complejo.Application.Dtos.User;
using Complejo.Application.Models.Identity.User;

namespace Complejo.Infrastructure.Automapper
{
    public partial class AutomapperProfile : Profile
    {
        public void UserAutomapperProfile()
        {
            CreateMapUserUserByFilterDto(); 
            CreateMapUserUserByIdDto();
        }

        private void CreateMapUserUserByFilterDto()
        {
            CreateMap<User, UserByFilterDto>()
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }

        private void CreateMapUserUserByIdDto()
        {
            CreateMap<User, UserByIdDto>();
        }
    }
}
