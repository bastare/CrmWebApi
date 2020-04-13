using AutoMapper;

using CrmWebApi.Helpers;

using Ninject;
using Ninject.Activation;
using Ninject.Modules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmWebApi.App_Start.IoC.Modules
{
	public class AutoMapperModule : NinjectModule
	{
		public override void Load()
		{
			var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<AutoMapperProfile>(); });
			Bind<IMapper>().ToConstructor( c => new Mapper( mapperConfiguration ) ).InSingletonScope();
		}
	}
}




