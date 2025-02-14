namespace HealthHorizon_API.Models
{
	public class Prescription
	{
		public int Id { get; set; }
		public string MedicationName { get; set; } = string.Empty;
		public string Dosage { get; set; } = string.Empty;
		public string Instructions {  get; set; } = string.Empty;

		public int AppointmentId { get; set; }
		public Appointment Appointment { get; set; } = null!;
	}
}
