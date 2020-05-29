using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Price : EntityBase
	{
		public string Name { get; set; }
		public string SiteLink { get; set; }
		public string ImageLink { get; set; }

	}
}
