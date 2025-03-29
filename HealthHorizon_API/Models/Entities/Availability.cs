namespace HealthHorizon_API.Models.Entities
{
    public class Availability
    {
        public int AvailabilityId { get; set; }
        public DateTime Date { get; set; }  // Specific date for the availability
        public DateTime AvailableFrom { get; set; }  // Start time
        public DateTime AvailableTo { get; set; }    // End time
        public bool IsAvailable { get; set; }  // Whether the time slot is available

        public List<DoctorAvailability> DoctorAvailabilities { get; set; }  // Navigation property
    }
}
