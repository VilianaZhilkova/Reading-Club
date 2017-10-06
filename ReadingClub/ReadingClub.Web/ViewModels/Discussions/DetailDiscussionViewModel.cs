using System;
using System.Collections.Generic;

using AutoMapper;

using ReadingClub.Data.Models;
using ReadingClub.Web.ViewModels.Books;
using ReadingClub.Web.ViewModels.Users;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.ViewModels.Discussions
{
    public class DetailDiscussionViewModel : IMapFrom<Discussion>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public BookViewModel Book { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Subject { get; set; }

        public int MaximumNumberOfParticipants { get; set; }

        public string Creator { get; set; }

        public ICollection<UserViewModel> Users { get; set; }

        //public ICollection<Comment> Comments { get; set; }
        public bool IsApproved { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Discussion, DetailDiscussionViewModel>()
                .ForMember(x => x.Creator, opt => opt.MapFrom(x => x.Creator.UserName));
        }
    }
}