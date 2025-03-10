using HealthHorizon_API.Models.PersonTypes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Medical_Record_Types
{
	public class InfectiousDiseaseTest
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public int MedicalRecordId { get; set; }
		public MedicalRecord MedicalRecord { get; set; } = null!;

		public bool Covid19 { get; set; } = false;
		public bool Influenza { get; set; } = false;
		public bool Tuberculosis { get; set; } = false;
		public bool HepatitisB { get; set; } = false;
		public bool HepatitisC { get; set; } = false;
		public bool HIV { get; set; } = false;
		public bool Syphilis { get; set; } = false;
		public bool Malaria { get; set; } = false;
		public bool Dengue { get; set; } = false;
		public bool LymeDisease { get; set; } = false;
		public bool ZikaVirus { get; set; } = false;
		public bool EpsteinBarrVirus { get; set; } = false;
		public bool Chlamydia { get; set; } = false;
		public bool Gonorrhea { get; set; } = false;
		public bool MRSA { get; set; } = false;

		public string TestMethod { get; set; } = string.Empty;
		public string Result { get; set; } = string.Empty;
		public string Notes {  get; set; } = string.Empty;	
	}
}
