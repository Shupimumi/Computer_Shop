using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Kit : EntityBase
	{
		//public Guid BrendID { get; set; }
		public List<Description> Descriptions { get; set; }
		//public Guid CategoryID { get; set; }
		public decimal Price { get; set; }
		public string ImageLink { get; set; }



	}
}
