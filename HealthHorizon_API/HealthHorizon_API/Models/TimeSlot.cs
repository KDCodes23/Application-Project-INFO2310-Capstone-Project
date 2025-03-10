using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HealthHorizon_API.Models.PersonTypes;

namespace HealthHorizon_API.Models
{
	public class TimeSlot
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;
	}
}
