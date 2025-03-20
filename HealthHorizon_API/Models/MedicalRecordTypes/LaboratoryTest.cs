using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HealthHorizon_API.Models.Entities;

namespace HealthHorizon_API.Models.Medical_Record_Types
{
	public class LaboratoryTest
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public int MedicalRecordId { get; set; }
		public MedicalRecord MedicalRecord { get; set; } = null!;

		public double Hemoglobin { get; set; } = 0;
		public double WhiteBloodCellCount { get; set; } = 0;
		public double PlateletCount { get; set; } = 0;
		public double RedBloodCellCount { get; set; } = 0;
		public double Glucose { get; set; } = 0;
		public double Sodium { get; set; } = 0;
		public double Potassium { get; set; } = 0;
		public double Calcium { get; set; } = 0;
		public double Cholesterol { get; set; } = 0;
		public double ALT { get; set; } = 0;
		public double AST { get; set; } = 0;
		public double Bilirubin { get; set; } = 0;
		public double Creatinine { get; set; } = 0;
		public double BloodUreaNitrogen { get; set; } = 0;
		public double GFR { get; set; } = 0;

		public bool UrineProtein { get; set; } = false;
		public bool UrineGlucose { get; set; } = false;
		public bool UrineKetones { get; set; } = false;

		public string TestLab { get; set; } = string.Empty;
		public string Notes { get; set; } = string.Empty;
	}
}
