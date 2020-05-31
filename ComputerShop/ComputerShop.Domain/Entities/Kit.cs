using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Kit : EntityBase
	{
		//Link one-to-many with Descriptions
		public List<Description> Descriptions { get; set; }
		//Link one-to-one with Brend
		public Brend Brend { get; set; }
		//Link one-to-one with Category
		public Category Category { get; set; }
		//Fields in table
		public decimal Price { get; set; }
		public string ImageLink { get; set; }
		//Link one-to-many with PriceList
		public PriceList PriceList { get; set; }
		public Guid PriceListId { get; set; }



	}
}
