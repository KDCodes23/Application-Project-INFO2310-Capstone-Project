namespace HealthHorizon_API.Models.DTOs
{
    public class AppointmentDTO
    {
        public Guid TimeSlotId { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
    }
}
