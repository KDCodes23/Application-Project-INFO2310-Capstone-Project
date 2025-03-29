namespace HealthHorizon_API.Models.Entities
{
    public class DoctorAvailability
    {
        public int DoctorAvailabilityId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan ShiftStart { get; set; } // Start time of the shift
        public TimeSpan ShiftEnd { get; set; }   // End time of the shift\
        public bool IsAvailable { get; set; }
    }
}