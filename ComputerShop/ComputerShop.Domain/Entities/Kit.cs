using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Kit : EntityBase
	{
		public List<Description> Descriptions { get; set; }
		public Guid CategoryId { get; set; }
		public Category Category { get; set; }
		[Column(TypeName = "Money")]
		public decimal Price { get; set; }
		public string ImageLink { get; set; }
		public DateTime CreatedDate { get; set; }



	}
}
