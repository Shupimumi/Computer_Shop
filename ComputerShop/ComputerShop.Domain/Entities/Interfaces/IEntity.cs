using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Entities.Interfaces
{
	public interface IEntity
	{
		Guid Id { get; set; }
	}
}
