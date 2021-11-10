using AutoMapper;
using Complejo.Application.Interfaces.Mapping;

namespace Complejo.Infrastructure.Automapper
{
    public class Mapping : IMapping
    {
        private readonly IMapper mapper;
        public Mapping(IMapper Mapper)
        {
            mapper = Mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            return mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
