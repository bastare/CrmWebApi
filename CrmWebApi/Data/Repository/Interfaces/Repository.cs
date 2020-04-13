using System;
using System.Collections.Generic;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CrmWebApi.Data.Interfaces
{
	public abstract class Repository
	{
		public IOrganizationService Service { get; }
		public string EntityName { get; }

		public Repository( IOrganizationService service , string entityName )
		{
			Service = service ??
				throw new ArgumentNullException( "Value can`t be a null" , nameof( Service ) );

			EntityName = entityName ??
				throw new ArgumentNullException( "Value can`t be a null" , nameof( EntityName ) );
		}

		public Entity GetById( Guid id , params string [ ] attr ) =>
			attr is null || attr.Length == 0
				? Service.Retrieve( EntityName , id , new ColumnSet( true ) )
				: Service.Retrieve( EntityName , id , new ColumnSet( attr ) );

		public Guid Create( Entity entity ) =>
			Service.Create(
				entity ??
					throw new ArgumentNullException( nameof( entity ) , "Value can`t be a null" )
			);

		public void Update( Entity entity ) =>
			Service.Update(
				entity ??
					throw new ArgumentNullException( nameof( entity ) , "Value can`t be a null" )
			);

		public void Delete( Guid id ) =>
			Service.Delete( EntityName , id );

		public IEnumerable<Entity> GetAll()
		{
			var query = new QueryExpression(EntityName)
			{
				ColumnSet = new ColumnSet(true)
			};

			return Service.RetrieveMultiple( query ).Entities;
		}
	}
}
