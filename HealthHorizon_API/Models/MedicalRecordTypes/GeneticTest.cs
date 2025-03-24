using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HealthHorizon_API.Models.Entities;

namespace HealthHorizon_API.Models.Medical_Record_Types
{
	public class GeneticTest
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public int MedicalRecordId { get; set; }
		public MedicalRecord MedicalRecord { get; set; } = null!;

		public bool CarrierScreening { get; set; } = false;
		public bool WholeExomeSequencing { get; set; } = false;
		public bool WholeGenomeSequencing { get; set; } = false;
		public bool PharmacogeneticTest { get; set; } = false;
		public bool CancerGeneticTest { get; set; } = false;
		public bool CardiovascularGeneticTest { get; set; } = false;
		public bool NeurologicalGeneticTest { get; set; } = false;
		public bool RareDiseaseTest { get; set; } = false;

		public string BRCA1Mutation { get; set; } = string.Empty;
		public string BRCA2Mutation { get; set; } = string.Empty;
		public string MTHFRMutation { get; set; } = string.Empty;
		public string APCMutation { get; set; } = string.Empty;
		public string LRRK2Mutation { get; set; } = string.Empty;
		public string CFTRMutation { get; set; } = string.Empty;
		public string HBBMutation { get; set; } = string.Empty;
		public string HTTMutation { get; set; } = string.Empty;
		public string LDLRMutation { get; set; } = string.Empty;

		public double DiabetesRiskScore { get; set; } = 0;
		public double HeartDiseaseRiskScore { get; set; } = 0;
		public double AlzheimerRiskScore { get; set; } = 0;
		public double ObesityRiskScore { get; set; } = 0;
		public string Notes { get; set; } = string.Empty;
	}
}
