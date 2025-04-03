using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HealthHorizon_API.Models.Entities
{
	public class Staff
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public string PhoneNumber { get; set; } = string.Empty;

		public Guid? RoleId { get; set; }
		public StaffRole? Role { get; set; } = null;

		[ForeignKey("ApplicationUser")]
		[Required]
		public string UserId { get; set; } = string.Empty;
		public IdentityUser? User { get; set; } = null;
	}
}
