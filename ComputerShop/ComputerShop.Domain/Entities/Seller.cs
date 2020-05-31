using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;//Надо ли чистить потом?
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Seller : EntityBase
	{
		[ForeignKey("Order")]
		public Order Order { get; set; }

		public string Name { get; set; }
		public string SiteLink { get; set; }
		public string ImageLink { get; set; }

	}
}
