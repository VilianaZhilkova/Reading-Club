using AutoMapper;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.Infrastructure
{
    public class MapperProvider : IMapperProvider
    {
        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}
