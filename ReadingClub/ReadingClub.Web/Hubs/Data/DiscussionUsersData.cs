using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Bytes2you.Validation;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Web.Hubs.Data
{
    public class DiscussionUsersData : IDiscussionUsersData
    {
        private readonly IRepository<Discussion> discussions;

        public DiscussionUsersData(IRepository<Discussion> discussions)
        {
            Guard.WhenArgument(discussions, nameof(discussions)).IsNull().Throw();
            this.discussions = discussions;
        }

        public void GetData()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ReadingClubDb"].ConnectionString);
            connection.Open();

            var query = this.discussions.GetAll.Select(x => x.Users);
            var commandText = query.ToString();
            var command = new SqlCommand(commandText, connection);

            SqlDependency dependency = new SqlDependency(command);
            dependency.OnChange += new OnChangeEventHandler(this.dependency_OnChange);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var reader = command.ExecuteReader();
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            ParticipantsHub.CheckForChanges();
        }
    }
}
