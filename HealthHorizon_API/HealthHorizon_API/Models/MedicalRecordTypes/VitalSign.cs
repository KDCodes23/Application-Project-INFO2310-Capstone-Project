using HealthHorizon_API.Models.PersonTypes;

namespace HealthHorizon_API.Models.Medical_Record_Types
{
	public class VitalSign
	{
		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public float HeartRate { get; set; } = 0;
		public float BloodPressureSystolic { get; set; } = 0;
		public float BloodPressureDiastolic { get; set; } = 0;
		public float RespiratoryRate { get; set; } = 0;
		public float Temperature { get; set; } = 0;
		public float OxygenSaturation { get; set; } = 0;
		public string MeasurementLocation { get; set; } = string.Empty;
		public string Notes { get; set; } = string.Empty;
	}
}
