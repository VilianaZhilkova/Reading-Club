using System;

using AutoMapper;

using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;
using ReadingClub.Web.ViewModels.Books;

namespace ReadingClub.Web.Areas.Administration.ViewModels.Discussions
{
    public class AdminDiscussionViewModel : IMapFrom<Discussion>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public BookViewModel Book { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Subject { get; set; }

        public int MaximumNumberOfParticipants { get; set; }

        public string Creator { get; set; }

        public int NumberOfParticipants { get; set; }

        public int NumberOfComments { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsApproved { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Discussion, AdminDiscussionViewModel>()
                .ForMember(x => x.Creator, opt => opt.MapFrom(x => x.Creator.UserName))
                .ForMember(x => x.NumberOfParticipants, opt => opt.MapFrom(x => x.Users.Count))
                .ForMember(x => x.NumberOfComments, opt => opt.MapFrom(x => x.Comments.Count));
        }
    }
}