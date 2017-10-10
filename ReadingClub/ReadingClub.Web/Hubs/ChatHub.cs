using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ReadingClub.Services.Data.Contracts;
using AutoMapper;
using Microsoft.AspNet.Identity;
using ReadingClub.Data.Models;

namespace ReadingClub.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ICommentsService commentsService;
        private readonly IUsersService usersService;
        private readonly IDiscussionsService discussionsService;
        private readonly IMapper mapper;

        public ChatHub(ICommentsService commentsService, IUsersService usersService, IDiscussionsService discussionsService, IMapper mapper)
        {
            this.commentsService = commentsService;
            this.usersService = usersService;
            this.discussionsService = discussionsService;
            this.mapper = mapper;
        }

        public void JoinVisitor(int discussionId)
        {
            Groups.Add(Context.ConnectionId, discussionId.ToString());
        }

        public void AddComment(string content, int discussionId)
        {
            var date = DateTime.UtcNow;
            var currentUserUserName = System.Web.HttpContext.Current.User.Identity.GetUserName();
            var currentUser = this.usersService.GetUserByUserName(currentUserUserName);
            var discussion = this.discussionsService.GetById(discussionId);

            this.commentsService.AddComment(content, date, currentUser, discussion);
            
            Clients.Group(discussionId.ToString()).AddNewCommentToPage(content, currentUserUserName);
        }        
    }
}