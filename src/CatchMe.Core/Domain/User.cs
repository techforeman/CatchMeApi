using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Core.Domain
{
	public class User : Entity
	{
		private static List<string> _roles = new List<string>()
		{
			"user", "admin"
		};
		public string Role { get; protected set; }
		public string Name { get; protected set; }
		public string Email { get; protected set; }
		public string Password { get; protected set; }
		public DateTime CreateAt { get; protected set; }

		public User(Guid id, string role, string name, string email, string password)
		{
			Id = id;
			SetRole(role);
			SetName(name);
			SetEmail(email);
			SetPassword(password);
			CreateAt = DateTime.UtcNow;

		}

		public void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new Exception($"User cannot have an empty name.");
			}
			Name = name;
			
		}

		public void SetEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				throw new Exception($"Email cannot have an empty name.");
			}
			Email = email;

		}

		public void SetRole(string role)
		{
			if (string.IsNullOrWhiteSpace(role))
			{
				throw new Exception($"User cannot have an empty role.");
			}
			role = role.ToLowerInvariant();
			if (_roles.Contains(role)==false)
			{
				throw new Exception($"User cannot have a role: {role}.");
			}
			Role = role;

		}

		public void SetPassword(string password)
		{
			if (string.IsNullOrWhiteSpace(password))
			{
				throw new Exception($"User cannot have an empty password.");
			}
			Password = password;

		}

	}
}
