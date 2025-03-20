using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HealthHorizon_API.Models.Identities;
using Microsoft.AspNetCore.Identity;

namespace HealthHorizon_API.Models.Entities
{
	public class Staff
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;

		public int RoleId { get; set; }
		public StaffRole Role { get; set; } = null!;

		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; } = string.Empty;
		public IdentityUser User { get; set; } = null!;
	}
}
