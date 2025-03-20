using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HealthHorizon_API.Models.Entities;

namespace HealthHorizon_API.Models.Medical_Record_Types
{
	public class BodyMeasurement
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public int MedicalRecordId { get; set; }
		public MedicalRecord MedicalRecord { get; set; } = null!;

		public double Height { get; set; } = 0;
		public double Weight { get; set; } = 0;
		public double ChestCircumference { get; set; } = 0;
		public double WaistCircumference { get; set; } = 0;
		public double HipCircumference { get; set; } = 0;
		public double NeckCircumference { get; set; } = 0;
		public double UpperArmCircumference { get; set; } = 0;
		public double ForearmCircumference { get; set; } = 0;
		public double ThighCircumference { get; set; } = 0;
		public double CalfCircumference { get; set; } = 0;
		public double BodyFatPercentage { get; set; } = 0;
		public double MuscleMass { get; set; } = 0;
		public string Notes { get; set; } = string.Empty;
	}
}
