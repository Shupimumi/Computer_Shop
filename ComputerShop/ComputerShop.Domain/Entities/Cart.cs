using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Cart : EntityBase
	{
		public Guid PriceListID { get; set; }
		public int Quantity { get; set; }

	}
}
