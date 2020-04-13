using AutoMapper;
using CrmWebApi.Data;
using CrmWebApi.DTOs;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CrmWebApi.Controllers
{
	[RoutePrefix( "api/Invoice" )]
	public class InvoiceController : ApiController
	{
		readonly IMapper _mapper;

		readonly InvoiceRepository _repo;

		public InvoiceController( IMapper mapper , InvoiceRepository repo )
		{
			_mapper = mapper
				?? throw new ArgumentNullException( typeof( IMapper ).FullName , "Ninject doesn`t bind, current type" );

			_repo = repo
				?? throw new ArgumentNullException( typeof( InvoiceRepository ).FullName , "Ninject doesn`t bind, current type" );
		}

		[HttpGet]
		[Route( nameof( GetInvoiceData ) )]
		public async Task<IHttpActionResult> GetInvoiceData()
		{
			IEnumerable<Entity> data = await _repo.GetInvoiceDataByStates( 2 );

			var result = _mapper.Map<IEnumerable<Entity>, IEnumerable<InvoiceDataForViewDTO>>( data );

			return Ok( result );
		}
	}
}