using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Customer : EntityBase
	{
		[ForeignKey("Order")]
		public Account Account { get; set; }
		public Order Order { get; set; }
		public string Surname { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Passport { get; set; }
		public string SecondName { get; set; }
		public string Email { get; set; }
	}
}
