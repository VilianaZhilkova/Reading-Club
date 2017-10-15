using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using ReadingClub.Data;
using ReadingClub.Web.Infrastructure.Mapping;

namespace ReadingClub.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MsSqlDbContext, Data.Migrations.Configuration>());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var mapper = new AutoMapperConfig();
            mapper.Execute(Assembly.GetExecutingAssembly());

            SqlDependency.Start(ConfigurationManager.ConnectionStrings["ReadingClubDb"].ConnectionString);
        }

        protected void Application_End()
        {
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["ReadingClubDb"].ConnectionString);
        }
    }
}
