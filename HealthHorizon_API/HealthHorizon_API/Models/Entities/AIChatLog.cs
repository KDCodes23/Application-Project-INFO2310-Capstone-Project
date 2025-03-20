using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Entities
{
	public class AIChatLog
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public List<string> Content { get; set; } = new List<string>();

		public int PatientId { get; set; }
		public Patient Patient { get; set; } = null!;
	}
}
