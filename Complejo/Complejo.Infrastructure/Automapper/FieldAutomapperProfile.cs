using AutoMapper;
using Complejo.Application.Commands.Field;
using Complejo.Application.Dtos.Field;
using Complejo.Application.Dtos.UiControls;
using Complejo.Domain.Entities;

namespace Complejo.Infrastructure.Automapper
{
    public partial class AutomapperProfile : Profile
    {
        public void FieldAutomapperProfile()
        {
            // to field
            CreateMapCreateFieldCommandField();
            CreateMapUpdateFieldCommandField();

            // from field
            CreateMapFieldFieldByIdDto();
            CreateMapFieldFieldByFilterDto();
            CreateMapFieldComboBoxDto();
        }

        private void CreateMapFieldComboBoxDto()
        {
            CreateMap<Field, ComboBoxDto>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Description));
        }

        private void CreateMapFieldFieldByFilterDto()
        {
            CreateMap<Field, FieldByFilterDto>()
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.FieldStatus.Description))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.FieldType.Description));
        }

        private void CreateMapUpdateFieldCommandField()
        {
            CreateMap<UpdateFieldCommand, Field>();
        }

        private void CreateMapFieldFieldByIdDto()
        {
            CreateMap<Field, FieldByIdDto>()
                .ForMember(dest => dest.IdStatusGroup, opts => opts.MapFrom(src => src.FieldStatus.IdFieldStatusGroup))
                .ForMember(dest => dest.IdStatus, opts => opts.MapFrom(src => src.IdFieldStatus))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.FieldStatus.Description))
                .ForMember(dest => dest.IdTypeGroup, opts => opts.MapFrom(src => src.FieldType.IdFieldTypeGroup))
                .ForMember(dest => dest.IdType, opts => opts.MapFrom(src => src.IdFieldType))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.FieldType.Description));
        }

        private void CreateMapCreateFieldCommandField()
        {
            CreateMap<CreateFieldCommand, Field>();
        }
    }
}
