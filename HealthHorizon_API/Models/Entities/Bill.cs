using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Entities
{
	public class Bill
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		public float Amount { get; set; } = 0f;
		[Required]
		public string PaymentMethod { get; set; } = string.Empty;
		[Required]
		public DateOnly Date { get; set; }

		[Required]
		public Guid AppointmentId { get; set; }
		public Appointment? Appointment { get; set; } = null;
	}
}
