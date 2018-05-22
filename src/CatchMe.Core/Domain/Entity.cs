using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Core.Domain
{
	public abstract class Entity
	{
		public Guid Id { get; protected set; }

		protected Entity()
		{
			Id = Guid.NewGuid();
		}
	}
}
