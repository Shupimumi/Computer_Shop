using ComputerShop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain
{
	public class ComputerShopContext : IdentityDbContext
	{
		public ComputerShopContext(DbContextOptions<ComputerShopContext> options) : base(options)
		{		
		}
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Kit> Kits { get; set; }
		public DbSet<Description> Descriptions { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }



	}
}
