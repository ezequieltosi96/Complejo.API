using AutoMapper;
using Complejo.Application.Commands.Turn;
using Complejo.Application.Dtos.Turn;
using Complejo.Domain.Entities;
using System;


namespace Complejo.Infrastructure.Automapper
{
    public partial class AutomapperProfile : Profile
    {
        public void TurnAutomapperProfile()
        {
            // to turn
            CreateMapCreateTurnCommandTurn();
            CreateMapCreateReservationLoggedInClientCommandTurn();
            CreateMapCreateReservationUnregisteredClientCommandTurn();

            // from turn
            CreateMapTurnTurnByFilterDto();
            CreateMapTurnTurnByIdDto();
            CreateMapTurnTurnByClientDto();
        }

        private void CreateMapCreateReservationUnregisteredClientCommandTurn()
        {
            CreateMap<CreateReservationUnregisteredClientCommand, Turn>()
                .ForMember(dest => dest.Time, opts => opts.Ignore());
        }

        private void CreateMapCreateReservationLoggedInClientCommandTurn()
        {
            CreateMap<CreateReservationLoggedInClientCommand, Turn>()
                .ForMember(dest => dest.Time, opts => opts.Ignore());
        }

        private void CreateMapTurnTurnByIdDto()
        {
            CreateMap<Turn, TurnByIdDto>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToShortDateString()))
                .ForMember(dest => dest.Time, opts => opts.MapFrom(src => src.Time.ToShortTimeString()))
                .ForMember(dest => dest.ClientName, opts => opts.MapFrom(src => src.Client.FullName))
                .ForMember(dest => dest.Field, opts => opts.MapFrom(src => src.Field.Description))
                .ForMember(dest => dest.FieldType, opts => opts.MapFrom(src => src.Field.FieldType.Description));
        }

        private void CreateMapCreateTurnCommandTurn()
        {
            CreateMap<CreateTurnCommand, Turn>()
                .ForMember(dest => dest.Time, opts => opts.Ignore());
        }

        private void CreateMapTurnTurnByFilterDto()
        {
            CreateMap<Turn, TurnByFilterDto>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToShortDateString()))
                .ForMember(dest => dest.Time, opts => opts.MapFrom(src => src.Time.ToShortTimeString()))
                .ForMember(dest => dest.Field, opts => opts.MapFrom(src => src.Field.Description))
                .ForMember(dest => dest.ClientName, opts => opts.MapFrom(src => src.Client.FullName));
        }

        private void CreateMapTurnTurnByClientDto()
        {
            CreateMap<Turn, TurnByClientDto>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToShortDateString()))
                .ForMember(dest => dest.Time, opts => opts.MapFrom(src => src.Time.ToShortTimeString()))
                .ForMember(dest => dest.Field, opts => opts.MapFrom(src => src.Field.Description))
                .ForMember(dest => dest.FieldType, opts => opts.MapFrom(src => src.Field.FieldType.Description));
        }
    }
}
