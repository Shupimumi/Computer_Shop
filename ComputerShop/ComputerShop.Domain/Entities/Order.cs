using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Order : EntityBase
	{
		public Receipt Receipt { get; set; }
		public Customer	Customer { get; set; }
		public Seller Seller { get; set; }

	}
}
