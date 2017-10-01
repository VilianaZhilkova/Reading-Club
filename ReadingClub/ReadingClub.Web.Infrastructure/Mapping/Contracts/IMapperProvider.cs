namespace ReadingClub.Web.Infrastructure.Mapping.Contracts
{
    public interface IMapperProvider
    {
        TDestination Map<TDestination>(object source);
    }
}
