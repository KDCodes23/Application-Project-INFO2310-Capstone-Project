using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHorizon_API.Models.Entities
{
	public class Patient
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		public string FirstName { get; set; } = string.Empty;
		[Required]
		public string LastName { get; set; } = string.Empty;
		[Required]
		public string Gender { get; set; } = string.Empty;
		[Required]
		public DateOnly? DateOfBirth { get; set; }
		[Required]
		public string PhoneNumber { get; set; } = string.Empty;

		public Guid? AddressId { get; set; }
		public Address? Address { get; set; } = null!;

		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; } = string.Empty;
		public IdentityUser? User { get; set; } = null;

	}
}
