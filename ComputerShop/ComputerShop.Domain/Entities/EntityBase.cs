using ComputerShop.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities
{
	public abstract class EntityBase : IEntity
	{
		public Guid Id { get; set; }

	}
}
