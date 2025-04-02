using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHorizon_API.Models.Entities
{
	public class Doctor
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		public string FirstName { get; set; } = string.Empty;
		[Required]
		public string LastName { get; set; } = string.Empty;
		[Required]
		public string Specialization { get; set; } = string.Empty;
		[Required]
		public string PhoneNumber { get; set; } = string.Empty;
		[Required]
		public string HospitalName { get; set; } = string.Empty;
		[Required]
		public string ProfessionalBio { get; set; } = string.Empty;

		public List<Schedule> Schedules { get; set; } = new List<Schedule>();

		[ForeignKey(nameof(User))]
		[Required]
		public string UserId { get; set; } = string.Empty;
		public IdentityUser? User { get; set; } = null;
	}
}
