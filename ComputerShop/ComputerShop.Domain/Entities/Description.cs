using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Description : EntityBase
	{
		//Link one-to-many with Kit
		public Kit Kit { get; set; }
		public Guid KitId { get; set; }
		//Fields in table
		public string Name { get; set; }
		public string Value { get; set; }
		

	}
}
