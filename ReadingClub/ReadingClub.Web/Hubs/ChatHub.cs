using System;

using Bytes2you.Validation;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

using ReadingClub.Common.Constants;
using ReadingClub.Services.Data.Contracts;

namespace ReadingClub.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ICommentsService commentsService;
        private readonly IUsersService usersService;
        private readonly IDiscussionsService discussionsService;

        public ChatHub(ICommentsService commentsService, IUsersService usersService, IDiscussionsService discussionsService)
        {
            Guard.WhenArgument(commentsService, nameof(commentsService)).IsNull().Throw();
            Guard.WhenArgument(usersService, nameof(usersService)).IsNull().Throw();
            Guard.WhenArgument(discussionsService, nameof(discussionsService)).IsNull().Throw();

            this.commentsService = commentsService;
            this.usersService = usersService;
            this.discussionsService = discussionsService;
        }

        public void JoinVisitor(int discussionId)
        {
            Guard.WhenArgument(discussionId, nameof(discussionId)).IsLessThanOrEqual(0).Throw();
            Groups.Add(Context.ConnectionId, discussionId.ToString());
        }

        public void AddComment(string content, int discussionId)
        {
            if (content.Length < StringLengthConstants.MinCommentContentLength || StringLengthConstants.MaxCommentContentLength < content.Length)
            {
                Clients.Client(Context.ConnectionId).SendError();
            }

            Guard.WhenArgument(discussionId, nameof(discussionId)).IsLessThanOrEqual(0).Throw();

            var date = DateTime.UtcNow;
            var currentUserUserName = System.Web.HttpContext.Current.User.Identity.GetUserName();
            var currentUser = this.usersService.GetUserByUserName(currentUserUserName);
            var discussion = this.discussionsService.GetById(discussionId);

            this.commentsService.AddComment(content, date, currentUser, discussion);
            
            Clients.Group(discussionId.ToString()).AddNewCommentToPage(content, currentUserUserName);
        }        
    }
}