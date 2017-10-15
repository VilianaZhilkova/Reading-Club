using AutoMapper;

using Microsoft.AspNet.SignalR;

using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.ViewModels.Discussions;

namespace ReadingClub.Web.Hubs
{
    public class ParticipantsHub : Hub
    {
        private readonly IDiscussionsService discussionsService;
        private readonly IMapper mapper;

        public ParticipantsHub(IDiscussionsService discussionsService, IMapper mapper)
        {
            this.discussionsService = discussionsService;
            this.mapper = mapper;
        }

        public static void CheckForChanges()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ParticipantsHub>();
            context.Clients.All.CheckForChanges();
        }

        public void JoinVisitor(int discussionId)
        {
            Groups.Add(Context.ConnectionId, discussionId.ToString());
        }

        public void UpdateParticipants(int discussionId)
        {
            var discussion = this.discussionsService.GetById(discussionId);
            var participants = this.mapper.Map<DetailDiscussionViewModel>(discussion).Users;

            Clients.Group(discussionId.ToString()).UpdateParticipantsList(participants);
        }
    }
}