﻿using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Identities
{
	public class Login
	{
		[Required]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
	}
}
