using ComputerShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain
{
	public class ComputerShopContext : DbContext
	{
		public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
		public ComputerShopContext(DbContextOptions<ComputerShopContext> options) : base(options)
		{		
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLoggerFactory(loggerFactory)  //tie-up DbContext with LoggerFactory object
				.EnableSensitiveDataLogging()
				.UseSqlServer(@"Server=DESKTOP-ESKKMMP;Database=Shop;Trusted_Connection=True;");
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
