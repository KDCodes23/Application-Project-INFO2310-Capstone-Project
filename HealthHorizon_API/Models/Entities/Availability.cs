using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Entities
{
    public class Availability
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
		public bool IsAvailable { get; set; } = false;

		public List<DoctorAvailability> DoctorAvailabilities { get; set; } = new List<DoctorAvailability>();
    }
}
