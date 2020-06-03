using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class OrderItem :EntityBase
	{		
		public Kit Kit { get; set; }
		public Guid KitId { get; set; }
		public Guid OrderId { get; set; }
		public Order Order { get; set; }
		public int Quantity { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
