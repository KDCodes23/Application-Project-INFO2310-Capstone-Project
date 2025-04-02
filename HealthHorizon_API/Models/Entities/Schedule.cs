namespace HealthHorizon_API.Models.Entities
{
	public class Schedule
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateOnly Date {  get; set; }
		public TimeOnly Start {  get; set; }
		public TimeOnly End { get; set; }

		public Guid DoctorId { get; set; }
		public Doctor? Doctor { get; set; } = null;

		public List<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
	}
}
