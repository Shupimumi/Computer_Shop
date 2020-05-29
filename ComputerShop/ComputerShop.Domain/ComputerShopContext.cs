using ComputerShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain
{
	public class ComputerShopContext : DbContext
	{
		public ComputerShopContext(DbContextOptions<ComputerShopContext> options) : base(options)
		{		
		}
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Kit> Kits { get; set; }
		public DbSet<Description> Descriptions { get; set; }



	}
}
