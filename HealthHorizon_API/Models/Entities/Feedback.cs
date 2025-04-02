using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Entities
{
	public class Feedback
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		public DateTime Date { get; set; }
		[Required]
		public string Details { get; set; } = string.Empty;
	}
}
