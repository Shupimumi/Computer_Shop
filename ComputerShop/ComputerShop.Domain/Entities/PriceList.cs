using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class PriceList : EntityBase
	{
		[ForeignKey("Cart")]
		//Link one-to-many with Kit
		public List<Kit> Kits { get; set; }
		//Link one-to-one with Cart
		public Cart Cart { get; set; }
		//Field in table
		public int Availiable { get; set; }

	}
}
