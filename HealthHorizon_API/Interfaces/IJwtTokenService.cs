﻿using Microsoft.AspNetCore.Identity;

namespace HealthHorizon_API.Interfaces
{
	public interface IJwtTokenService
	{
		public Task<string> GenerateJwtTokenAsync(IdentityUser user);
	}
}
