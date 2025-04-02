using HealthHorizon_API.Models.Entities;

namespace HealthHorizon_API.Models.DTOs
{
	public class MedicalRecordSmallDTO
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public int PatientId { get; set; }
		public Patient Patient { get; set; } = null!;
		public DateTime Date { get; set; }
	}
}
