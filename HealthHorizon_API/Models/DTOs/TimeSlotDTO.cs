namespace HealthHorizon_API.Models.DTOs
{
	public class TimeSlotDTO
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public TimeOnly Start { get; set; }
		public TimeOnly End { get; set; }
		public bool IsAvailible { get; set; } = false;

		public Guid PatientId { get; set; } = Guid.Empty;

		public Guid ScheduleId { get; set; }
	}
}
