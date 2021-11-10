using AutoMapper;
using Complejo.Application.Dtos.UiControls;
using Complejo.Domain.Entities;

namespace Complejo.Infrastructure.Automapper
{
    public partial class AutomapperProfile : Profile
    {
        public void FieldTypeAutomapperProfile()
        {
            CreateMapFieldTypeComboBoxDto();
        }

        private void CreateMapFieldTypeComboBoxDto()
        {
            CreateMap<FieldType, ComboBoxDto>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Description));
        }
    }
}
