using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Entities
{
    public class DoctorAvailability
    {
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		public DateOnly Date { get; set; }
		[Required]
		public TimeSpan Start { get; set; }
		[Required]
		public TimeSpan End { get; set; }
		[Required]
		public bool IsAvailable { get; set; }

		[Required]
		public Guid DoctorId { get; set; }
		public Doctor? Doctor { get; set; } = null!;
	}
}