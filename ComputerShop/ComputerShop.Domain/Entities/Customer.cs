using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Customer : EntityBase
	{
		public string Surname { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Passport { get; set; }
		public string SecondName { get; set; }
		public string Email { get; set; }
	}
}
