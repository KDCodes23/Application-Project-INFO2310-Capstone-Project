namespace HealthHorizon_API.Models.DTOs
{
	public class StaffDTO
	{
		public string Name { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public Guid? RoleId { get; set; }
	}
}
