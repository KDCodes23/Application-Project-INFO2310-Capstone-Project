namespace HealthHorizon_API.Models.Entities
{
	public class TimeSlot
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public TimeOnly Start {  get; set; }
		public TimeOnly End { get; set; }
		public bool IsAvailible { get; set; } = false;

		public Guid PatientId { get; set; } = Guid.Empty;
		public Patient? Patient { get; set; } = null;

		public Guid ScheduleId { get; set; }
		public Schedule? Schedule { get; set; } = null;
	}
}
