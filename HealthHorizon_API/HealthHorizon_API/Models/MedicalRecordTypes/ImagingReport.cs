using HealthHorizon_API.Models.PersonTypes;

namespace HealthHorizon_API.Models.Medical_Record_Types
{
	public class ImagingReport
	{
		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public bool XRay { get; set; } = false;
		public bool CTScan { get; set; } = false;
		public bool MRI { get; set; } = false;
		public bool Ultrasound { get; set; } = false;
		public bool Mammogram { get; set; } = false;
		public bool PETScan { get; set; } = false;
		public bool BoneDensityScan { get; set; } = false;
		public bool Echocardiogram { get; set; } = false;
		public bool DopplerUltrasound { get; set; } = false;

		public string GeneralFindings { get; set; } = string.Empty;
		public string Impression { get; set; } = string.Empty;
		public string RadiologistNotes { get; set; } = string.Empty;

		public double TumorSize { get; set; } = 0;
		public double AneurysmDiameter { get; set; } = 0;
		public double BoneDensity { get; set; } = 0;
		public double EjectionFraction { get; set; } = 0;
	}
}
