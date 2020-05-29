using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ComputerShop.Models;
using ComputerShop.Domain;

namespace ComputerShop.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ComputerShopContext _context;

		public HomeController(ILogger<HomeController> logger, ComputerShopContext dbContext)
		{
			_logger = logger;
			_context = dbContext;
			//_context.Descriptions.Add(new Domain.Entities.Description()
			//{
			//	Name = "TestDescription",
			//	Value = "test",
			//	KitID = Guid.Empty

			//});
			//_context.Kits.Add(new Domain.Entities.Kit()
			//{
			//	ImageLink = " ",
			//	Price = 3m,
			//	Descriptions = new List<Domain.Entities.Description>()
			//	{
			//		new Domain.Entities.Description()
			//	{
			//		Name = "TestDescription",
			//		Value = "test",
			//		KitID = Guid.Empty

			//	}		
			//	}
			//});
			//_context.SaveChanges();
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
