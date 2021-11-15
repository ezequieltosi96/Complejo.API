using AutoMapper;
using Complejo.Application.Commands.Client;
using Complejo.Domain.Entities;

namespace Complejo.Infrastructure.Automapper
{
    public partial class AutomapperProfile : Profile
    {
        public void ClientAutommaperProfile()
        {
            CreateMapCreateClientCommandClient();
        }

        private void CreateMapCreateClientCommandClient()
        {
            CreateMap<CreateClientCommand, Client>()
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
