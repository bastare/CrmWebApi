[assembly: WebActivatorEx.PreApplicationStartMethod( typeof( CrmWebApi.App_Start.NinjectWebCommon ) , "Start" )]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute( typeof( CrmWebApi.App_Start.NinjectWebCommon ) , "Stop" )]

namespace CrmWebApi.App_Start
{
	using System;
	using System.Web;
	using System.Web.Http;

	using Microsoft.Web.Infrastructure.DynamicModuleHelper;

	using Ninject;
	using Ninject.Web.Common;
	using Ninject.Web.Common.WebHost;
	using Ninject.WebApi.DependencyResolver;

	using CrmWebApi.App_Start.IoC.Modules;

	using System.Linq;
	using CrmWebApi.Data;
	using KostenVoranSchlagConsoleParser.Api;

	public static class NinjectWebCommon
	{
		static readonly Bootstrapper bootstrapper = new Bootstrapper();

		public static void Start()
		{
			DynamicModuleUtility.RegisterModule( typeof( OnePerRequestHttpModule ) );
			DynamicModuleUtility.RegisterModule( typeof( NinjectHttpModule ) );

			bootstrapper.Initialize( CreateKernel );
		}

		static void Stop() =>
			bootstrapper.ShutDown();


		static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();

			try
			{
				kernel.StartupInit();

				kernel.RegisterModules();

				kernel.RegisterServices();

				GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver( kernel );

				return kernel;
			}
			catch
			{
				kernel.Dispose();
				throw;
			}
		}

		static void RegisterServices( this IKernel kernel )
		{
			kernel.Bind<InvoiceRepository>().ToSelf().InTransientScope()
				.WithConstructorArgument( "service" , ConnectHelper.CrmService );
		}

		static void RegisterModules( this IKernel kernel ) =>
			kernel.Load( new AutoMapperModule() );

		static void StartupInit( this IKernel kernel )
		{
			kernel.Bind<Func<IKernel>>().ToMethod( ctx => () => new Bootstrapper().Kernel );
			kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
		}
	}
}