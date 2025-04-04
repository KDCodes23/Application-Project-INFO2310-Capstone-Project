namespace HealthHorizon_API.Models.Entities
{
	public class TimeSlot
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public TimeOnly Start {  get; set; }
		public TimeOnly End { get; set; }
		public bool IsAvailable { get; set; } = true;

		public Guid DoctorId { get; set; } = Guid.Empty;
		public Doctor? Doctor { get; set; } = null;

		public Guid ScheduleId { get; set; }
		public Schedule? Schedule { get; set; } = null;
	}
}
