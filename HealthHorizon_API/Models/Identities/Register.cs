using HealthHorizon_API.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Identities
{
	public class Register
	{
		[Required]
		public string UserName { get; set; } = string.Empty;
		[Required]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;

		public Doctor? Doctor { get; set; }
		public Staff? Staff { get; set; }
		public Patient? Patient { get; set; }
	}
}
