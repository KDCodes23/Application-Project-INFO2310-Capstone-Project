namespace HealthHorizon_API.Models
{
	public class Doctor
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Specialization { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public Schedule Schedule { get; set; } = null!;
	}
}
