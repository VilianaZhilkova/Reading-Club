using System;

using AutoMapper;

using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.ViewModels.Books
{
    public class BookViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, BookViewModel>()
                .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.Name));
        }
    }
}