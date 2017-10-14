using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace ReadingClub.Web.Areas.Administration.ViewModels.Comments
{
    public class AdminCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public string Content { get; set; }

        public int DiscussionId { get; set; }

        public string DiscussionSubject { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, AdminCommentViewModel>()
                 .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.UserName))
                 .ForMember(x => x.DiscussionId, opt => opt.MapFrom(x => x.Discussion.Id))
                 .ForMember(x => x.DiscussionSubject, opt => opt.MapFrom(x => x.Discussion.Subject));
        }
    }
}