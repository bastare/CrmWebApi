using AutoMapper;
using CrmWebApi.DTOs;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmWebApi.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Entity , InvoiceDataForViewDTO>()
				.ForMember( dest => dest.Name , opt => opt.MapFrom( src => src.GetAttributeValue<string>( "name" ) ) )
				.ForMember( dest => dest.Amount , opt => opt.MapFrom( src => src.GetAttributeValue<Money>( "totalamount" ).Value ) )
				.ForMember( dest => dest.Date , opt => opt.MapFrom( src => src.GetAttributeValue<DateTime?>( "new_dateoncomplited" ) ) )
				.ForMember( dest => dest.CurrencyType , opt => opt.MapFrom( src => src.FormattedValues [ "transactioncurrencyid" ] ) )
				.ForMember( dest => dest.AmountView , opt => opt.MapFrom( src => src.FormattedValues [ "totalamount" ] ) );
		}
	}
}