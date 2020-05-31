using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Receipt : EntityBase
	{
		[ForeignKey("Order")]
		//Link one-to-one with Cart
		public Cart Cart { get; set; }
		//Field in table
		public decimal Amount { get; set; }

	}
}
