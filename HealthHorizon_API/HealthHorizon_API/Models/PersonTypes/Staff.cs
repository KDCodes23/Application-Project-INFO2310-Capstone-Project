namespace HealthHorizon_API.Models.PersonTypes
{
	public class Staff
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;

		public int RoleId { get; set; }
		public StaffRole Role { get; set; } = null!;
	}
}
