using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Entities
{
	public class Appointment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Status { get; set; } = string.Empty;

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public int PatientId { get; set; }
		public Patient? Patient { get; set; } = null;
	}
}
