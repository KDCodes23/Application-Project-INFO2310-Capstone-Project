using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Entities
{
	public class Prescription
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		public string MedicationName { get; set; } = string.Empty;
		[Required]
		public string Dosage { get; set; } = string.Empty;
		[Required]
		public string Instructions { get; set; } = string.Empty;

		[Required]
		public Guid AppointmentId { get; set; }
		public Appointment? Appointment { get; set; } = null;
	}
}
