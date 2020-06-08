using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Order : EntityBase
	{
		// Todo remove
		//public Guid InvoiceId { get; set; }
		//public Invoice Invoice { get; set; }
		//public Guid AccountId { get; set; }
		//public Account Account { get; set; }
		public Guid CustomerId { get; set; }
		public Customer Customer { get; set; }
		public List<OrderItem> OrderItems { get; set; }
		//Doto remove
		//public int Quantity { get; set; }
		public OrderStatus Status { get; set; }
		public DateTime CreatedDate { get; set; }

	}

	public enum OrderStatus
	{
		Active,
		Paid,
		Canceled
	}
}
