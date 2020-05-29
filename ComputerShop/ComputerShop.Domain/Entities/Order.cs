using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Order : EntityBase
	{
		public Guid CustomerID { get; set; }
		public Guid SellerID { get; set; }
		public Guid ReceiptID { get; set; }
	}
}
