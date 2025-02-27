namespace HealthHorizon_API.Models.PersonTypes
{
	public class Bill
	{
		public int Id { get; set; }
		public float Amount { get; set; } = 0f;
		public string PaymentMethod { get; set; } = string.Empty;
		public DateTime Date { get; set; }

		public int AppointmentId { get; set; }
		public Appointment Appointment { get; set; } = null!;
	}
}
