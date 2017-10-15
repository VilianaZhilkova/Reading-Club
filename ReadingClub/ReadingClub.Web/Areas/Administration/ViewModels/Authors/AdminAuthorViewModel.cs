using AutoMapper;

using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.Areas.Administration.ViewModels.Authors
{
    public class AdminAuthorViewModel : IMapFrom<Author>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfBooks { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Author, AdminAuthorViewModel>()
                 .ForMember(x => x.NumberOfBooks, opt => opt.MapFrom(x => x.Books.Count));
        }
    }
}