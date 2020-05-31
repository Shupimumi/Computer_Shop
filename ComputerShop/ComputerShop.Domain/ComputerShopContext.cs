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
		public DbSet<Order> Orders { get; set; }	
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Brend> Brends { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<PriceList> PriceLists { get; set; }
		public DbSet<Receipt> Receipts { get; set; }
		public DbSet<Seller> Sellers { get; set; }



	}
}
