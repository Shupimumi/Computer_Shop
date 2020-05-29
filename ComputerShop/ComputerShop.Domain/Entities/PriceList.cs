using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class PriceList : EntityBase
	{
		public Guid PriceListID { get; set; }
		public Guid KitID { get; set; }
		public int Availiable { get; set; }

	}
}
