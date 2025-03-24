using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHorizon_API.Models.Entities
{
	public class Doctor
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Specialization { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string HospitalName { get; set; } = string.Empty;
		public string ProfessionalBio { get; set; } = string.Empty;

		[ForeignKey(nameof(User))]
		public string UserId { get; set; } = string.Empty;
		public IdentityUser? User { get; set; } = null;
	}
}
