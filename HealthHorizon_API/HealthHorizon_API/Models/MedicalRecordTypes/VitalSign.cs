using HealthHorizon_API.Models.PersonTypes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Medical_Record_Types
{
	public class VitalSign
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public int MedicalRecordId { get; set; }
		public MedicalRecord MedicalRecord { get; set; } = null!;

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
