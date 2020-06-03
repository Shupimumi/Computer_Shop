﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public class Category : EntityBase
	{
		public Kit Kit { get; set; }
		public string Name { get; set; }
		public DateTime CreatedDate { get; set; }

	}
}
