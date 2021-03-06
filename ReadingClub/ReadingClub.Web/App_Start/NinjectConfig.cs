[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ReadingClub.Web.App_Start.NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ReadingClub.Web.App_Start.NinjectConfig), "Stop")]

namespace ReadingClub.Web.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    
    using AutoMapper;

    using Data;
    using Data.Common;
    using Data.Common.Contracts;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;

    using Services.Data.Contracts;
    using Services.Web;
    using Services.Web.Contracts;

    public static class NinjectConfig 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                GlobalHost.DependencyResolver = new NinjectSignalRDependencyResolver(kernel);
                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(x =>
            {
                x.FromThisAssembly()
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            kernel.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(IService))
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            kernel.Bind(typeof(DbContext), typeof(MsSqlDbContext)).To<MsSqlDbContext>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>)).InRequestScope();
            kernel.Bind<IUnitOfWork>().To<EfUnitOfWork>().InRequestScope();

            kernel.Bind<IMapper>().ToMethod(ctx => Mapper.Instance);
            kernel.Bind<ICacheService>().To<HttpCacheService>().InRequestScope();

            kernel.Bind(typeof(ApplicationUserManager)).ToSelf().InRequestScope();
            kernel.Bind(typeof(IUserStore<>)).To(typeof(UserStore<>)).InRequestScope();
        }

        private class NinjectSignalRDependencyResolver : DefaultDependencyResolver
        {
            private readonly IKernel kernel;

            public NinjectSignalRDependencyResolver(IKernel kernel)
            {
                this.kernel = kernel;
            }

            public override object GetService(Type serviceType)
            {
                return this.kernel.TryGet(serviceType) ?? base.GetService(serviceType);
            }

            public override System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
            {
                return this.kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
            }
        }
    }
}
