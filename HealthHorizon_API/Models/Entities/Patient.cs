using HealthHorizon_API.Models.Identities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHorizon_API.Models.Entities
{
	public class Patient
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; }
		public string PhoneNumber { get; set; } = string.Empty;

		public int AddressId { get; set; }
		public Address? Address { get; set; }

		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; } = string.Empty;
		public IdentityUser? User { get; set; } = null;

	}
}
