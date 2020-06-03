using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Description : EntityBase
	{
		public Kit Kit { get; set; }
		public Guid KitId { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public DateTime CreatedDate { get; set; }


	}
}
