using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace ReadingClub.Web.ViewModels.Comments
{
    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {

        public DateTime Date { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.UserName));
        }
    }
}