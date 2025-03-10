using HealthHorizon_API.Models.Medical_Record_Types;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.PersonTypes
{
	public class MedicalRecord
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Notes { get; set; } = string.Empty;
		public DateTime Date { get; set; }

		public int PatientId { get; set; }
		public Patient Patient { get; set; } = null!;

		public List<AllergyTest> AllergyTests { get; set; } = new List<AllergyTest>();
		public List<BodyMeasurement> BodyMeasurements { get; set; } = new List<BodyMeasurement>();
		public List<CardiacTest> CardiacTests { get; set; } = new List<CardiacTest>();
		public List<EndocrineTest> EndocrineTests { get; set; } = new List<EndocrineTest>();
		public List<GeneticTest> GeneticTests { get; set; } = new List<GeneticTest>();
		public List<ImagingReport> ImagingReports { get; set; } = new List<ImagingReport>();
		public List<InfectiousDiseaseTest> InfectiousDiseaseTests { get; set; } = new List<InfectiousDiseaseTest>();
		public List<LaboratoryTest> LaboratoryTests { get; set; } = new List<LaboratoryTest>();
		public List<NeurologicalTest> NeurologicalTests { get; set; } = new List<NeurologicalTest>();
		public List<RespiratoryTest> RespiratoryTests { get; set; } = new List<RespiratoryTest>();
		public List<VitalSign> VitalSigns { get; set; } = new List<VitalSign>();
	}
}
