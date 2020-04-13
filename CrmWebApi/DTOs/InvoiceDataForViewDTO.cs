using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmWebApi.DTOs
{
	public class InvoiceDataForViewDTO
	{
		public string Id { get; set; }
		public decimal? Amount { get; set; }
		public DateTime? Date { get; set; }

		public string Name { get; set; }
		public string CurrencyType { get; set; }
		public string AmountView { get; set; }
	}
}