using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Entities
{
	public class AIChatLog
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		public DateTime Date { get; set; }
		[Required]
		public List<string> Content { get; set; } = new List<string>();

		[Required]
		public Guid PatientId { get; set; }
		public Patient? Patient { get; set; } = null;
	}
}
