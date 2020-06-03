using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Order : EntityBase
	{
		public Guid InvoiceId { get; set; }
		public Invoice Invoice { get; set; }
		public Guid AccountId { get; set; }
		public Account Account { get; set; }
		public List<OrderItem> OrderItems { get; set; }
		public int Quantity { get; set; }
		public int Status { get; set; }
		public DateTime CreatedDate { get; set; }

	}
}
