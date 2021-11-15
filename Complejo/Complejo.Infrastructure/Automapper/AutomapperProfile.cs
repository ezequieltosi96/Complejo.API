using AutoMapper;

namespace Complejo.Infrastructure.Automapper
{
    public partial class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            FieldAutomapperProfile();
            FieldStatusAutomapperProfile();
            FieldTypeAutomapperProfile();

            UserAutomapperProfile();

            TurnAutomapperProfile();

            ClientAutommaperProfile();
        }
    }
}
