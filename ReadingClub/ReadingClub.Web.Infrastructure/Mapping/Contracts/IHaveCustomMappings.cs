using AutoMapper;

namespace ReadingClub.Web.Infrastructure.Mapping.Contracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
