using System;

namespace HealthHorizon_API.Models.Entities
{
    public class DoctorSlot
    {
        public int DoctorSlotId { get; set; }
        public int DoctorId { get; set; }
        public DateTime SlotDate { get; set; }      // The date for the slot
        public TimeSpan SlotStart { get; set; }     // Start time of the slot
        public TimeSpan SlotEnd { get; set; }       // End time of the slot (typically SlotStart + 1 hour)
        public bool IsAvailable { get; set; }       // Is the slot available for appointments
    }
}
