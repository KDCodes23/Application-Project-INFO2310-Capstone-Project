using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Entities
{
	public class Prescription
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string MedicationName { get; set; } = string.Empty;
		public string Dosage { get; set; } = string.Empty;
		public string Instructions { get; set; } = string.Empty;

		public int AppointmentId { get; set; }
		public Appointment Appointment { get; set; } = null!;
	}
}
