using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Entities
{
	public class Appointment
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		public DateTime Date { get; set; }
		[Required]
		public string Status { get; set; } = string.Empty;

		[Required]
		public Guid TimeSlotId { get; set; }
        public TimeSlot? TimeSlot { get; set; } = null!;

		[Required]
		public Guid DoctorId { get; set; }
		public Doctor? Doctor { get; set; } = null!;

		[Required]
		public Guid PatientId { get; set; }
		public Patient? Patient { get; set; } = null;
	}
}
