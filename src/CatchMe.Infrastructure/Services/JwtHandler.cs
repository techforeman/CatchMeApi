﻿using CatchMe.Infrastructure.DTO;
using CatchMe.Infrastructure.Extensions;
using CatchMe.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Services
{
	public class JwtHandler : IJwtHandler
	{
		private readonly JwtSettings _jwtSettings;

		public JwtHandler(IOptions<JwtSettings> jwtSettings)
		{
			_jwtSettings = jwtSettings.Value;
		}
		public JwtDTO CreateToken(Guid userId, string role)
		{
			var now = DateTime.UtcNow;
			var claims = new Claim[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
				new Claim(ClaimTypes.Role, role),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString())

			};

			var expires = now.AddMinutes(_jwtSettings.ExpiryMinutes);
			var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
				SecurityAlgorithms.HmacSha256);
			var jwt = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				claims: claims,
				notBefore: now,
				expires: expires,
				signingCredentials: signingCredentials
				);
			var token = new JwtSecurityTokenHandler().WriteToken(jwt);

			return new JwtDTO
			{
				Token = token,
				Expires = expires.ToTimestamp()
			};
		}
	}
}
