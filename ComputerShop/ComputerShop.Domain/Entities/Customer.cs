using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Customer : EntityBase
	{

		public Account Account { get; set; }
		public List<Order> Orders { get; set; }
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
		public string Name { get; set; }
		public string SecondName { get; set; }
		public string Phone { get; set; }
		public string Passport { get; set; }

		public string Email { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
