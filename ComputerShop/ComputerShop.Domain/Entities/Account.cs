using ComputerShop.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Account : EntityBase
	{
		public List<Invoice> Invoices { get; set; }
		public Guid CustomerId { get; set; }
		public Customer Customer { get; set; }
		[Column(TypeName = "Money")]
		public double Amount { get; set; }
		public string CardNumber { get; set; }
		public string Currency { get; set; }
		public DateTime CreatedDate { get; set; }

	}
}
