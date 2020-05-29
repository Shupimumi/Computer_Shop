using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Description : EntityBase
	{

		public string Name { get; set; }
		public string Value { get; set; }
		public Guid KitID { get; set; }
		public Kit Kit { get; set; }

	}
}
