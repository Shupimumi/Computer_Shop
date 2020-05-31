using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Cart : EntityBase
	{
		[ForeignKey("Receipt")]
		//Link one-to-one with Cart
		public PriceList PriceList { get; set; }
		//Field in table
		public int Quantity { get; set; }

	}
}
