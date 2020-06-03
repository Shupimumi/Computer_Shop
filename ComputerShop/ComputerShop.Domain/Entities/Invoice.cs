using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Invoice : EntityBase
	{
		//public Guid AccountId { get; set; }
		//public Account Account { get; set; }
		public Order Order { get; set; }
		[Column(TypeName = "Money")]
		public decimal Amount { get; set; }
		public DateTime CreatedDate { get; set; }

	}
}
