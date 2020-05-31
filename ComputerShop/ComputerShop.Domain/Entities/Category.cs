using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Category : EntityBase
	{
		[ForeignKey("Kit")]
		//Link one-to-one with Kit
		public Kit Kit { get; set; }
		//Fields in table
		public string Name { get; set; }

	}
}
