using HealthHorizon_API.Models.Entities;

namespace HealthHorizon_API.Models.Identities
{
	public class Register
	{
		public string UserName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;

		public Doctor? Doctor { get; set; }
		public Staff? Staff { get; set; }
		public Patient? Patient { get; set; }
	}
}
