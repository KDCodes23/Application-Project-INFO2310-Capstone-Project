namespace HealthHorizon_API.Models
{
	public class MedicalRecord
	{
		public int Id { get; set; }
		public string Detals { get; set; } = string.Empty;
		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public int PatientId { get; set; }
		public Patient Patient { get; set; } = null!;
	}
}
