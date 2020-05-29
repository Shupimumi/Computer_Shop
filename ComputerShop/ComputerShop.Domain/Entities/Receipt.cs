using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Receipt : EntityBase
	{
		public Guid CartID { get; set; }
		public decimal Amount { get; set; }

	}
}
