namespace HealthHorizon_API.Models
{
	public class AIChatLog
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public List<string> Content { get; set; } = new List<string>();

		public int PatientId { get; set; }
		public Patient Patient { get; set; } = null!;
	}
}
