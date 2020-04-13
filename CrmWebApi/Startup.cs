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
			string root = AppDomain.CurrentDomain.BaseDirectory;

			var physicalFileSystem = new PhysicalFileSystem(Path.Combine(root, "wwwroot"));

			var options = new FileServerOptions
			{
				RequestPath = PathString.Empty,
				EnableDefaultFiles = true,
				FileSystem = physicalFileSystem
			};
			options.StaticFileOptions.FileSystem = physicalFileSystem;
			options.StaticFileOptions.ServeUnknownFileTypes = false;

			app.UseFileServer( options );
		}
	}
}
