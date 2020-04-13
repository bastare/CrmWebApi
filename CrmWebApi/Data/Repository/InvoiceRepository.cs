using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using CrmWebApi.Data.Interfaces;

namespace CrmWebApi.Data
{
	public class InvoiceRepository : Repository
	{
		const string ENTITY_NAME = "invoice";

		readonly IOrganizationService _service;

		public InvoiceRepository( IOrganizationService service )
			: base( service , ENTITY_NAME )
		{
			_service = service;
		}

		public async Task<DataCollection<Entity>> GetInvoiceDataByStates( int state )
		{
			var query = new QueryExpression
			{
				EntityName = ENTITY_NAME,

				ColumnSet = new ColumnSet("invoiceid", "name", "totalamount", "new_dateoncomplited"),

				Criteria = new FilterExpression
				{
					Conditions =
					{
						new ConditionExpression
						{
							AttributeName = "statecode",
							Operator = ConditionOperator.Equal,
							Values = { state }
						}
					}
				}
			};

			var result = await Task.Run( () => _service.RetrieveMultiple(query)?.Entities
				?? throw new ArgumentNullException("dat bad", new InvalidPluginExecutionException() ));

			return result;
		}
	}
}