using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ReadingClub.Web.Hubs.Data
{
    public class DiscussionUsersData : IDiscussionUsersData
    {
        public void GetData()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ReadingClubDb"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT d.Discussion_Id, d.User_Id ,u.UserName
                    FROM dbo.DiscussionUsers d
                    JOIN dbo.AspNetUsers u
                    ON d.User_Id = u.Id", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    var reader = command.ExecuteReader();
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            ParticipantsHub.CheckForChanges();
        }
    }
}

