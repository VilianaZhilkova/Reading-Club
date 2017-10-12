using System;

using AutoMapper;
using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.ViewModels.Discussions
{
    public class DiscussionViewModel : IMapFrom<Discussion>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Subject { get; set; }

        public string BookTitle { get; set; }

        public string BookAuthor { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

          public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Discussion, DiscussionViewModel>()
                .ForMember(x => x.BookTitle, opt => opt.MapFrom(x => x.Book.Title))
                .ForMember(x => x.BookAuthor, opt => opt.MapFrom(x => x.Book.Author.Name));
        }
    }
}