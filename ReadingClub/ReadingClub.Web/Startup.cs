using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReadingClub.Web.Startup))]
namespace ReadingClub.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
