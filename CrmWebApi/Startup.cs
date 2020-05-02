using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup( typeof( CrmWebApi.Startup ) )]

namespace CrmWebApi
{
	public class Startup
	{
		public void Configuration( IAppBuilder app )
		{

			app.UseFileServer( new FileServerOptions
			{
				RequestPath = PathString.Empty ,
				EnableDefaultFiles = true ,
				FileSystem = new PhysicalFileSystem( $"{ AppDomain.CurrentDomain.BaseDirectory}wwwroot" )
			} );

			app.UseStaticFiles();
		}
	}
}
