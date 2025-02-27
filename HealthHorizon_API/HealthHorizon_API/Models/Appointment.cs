namespace HealthHorizon_API.Models.PersonTypes
{
	public class Appointment
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Status { get; set; } = string.Empty;

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public int PatientId { get; set; }
		public Patient Patient { get; set; } = null!;
	}
}
