using ComputerShop.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;//Надо ли чистить потом?
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Account : EntityBase
	{
		[ForeignKey("Customer")]
		public Customer Customer { get; set; }
		public double Amount { get; set; }
		public string CardNumber { get; set; }
		public string Currency { get; set; }
		
	}
}
