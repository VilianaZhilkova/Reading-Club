using AutoMapper;

using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.Areas.Administration.ViewModels.Books
{
    public class AdminBookViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int NumberOfDiscussions { get; set; }

        public bool IsApproved { get; set; }

        public bool IsDeleted { get; set; }
        
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, AdminBookViewModel>()
                 .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.Name))
                 .ForMember(x => x.NumberOfDiscussions, opt => opt.MapFrom(x => x.Discussions.Count));
        }
    }
}