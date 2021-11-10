
namespace Complejo.Application.Interfaces.Mapping
{
    public interface IMapping
    {
        TDestination Map<TDestination>(object source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
