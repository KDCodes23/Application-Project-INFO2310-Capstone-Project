using HealthHorizon_API.Data;
using HealthHorizon_API.Models.DTOs;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TimeSlotController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public TimeSlotController(HealthHorizonContext context)
		{
			this.context = context;
		}

        [HttpPost("schedule/doctor/{doctorId}/all")]
        public async Task<IActionResult> CreateDoctorTimeSlots([FromRoute] Guid doctorId)
        {
            // Fetch the doctor's schedule to verify their availability
            var doctorSchedule = await context.Schedules
                .FirstOrDefaultAsync(s => s.DoctorId == doctorId);

            if (doctorSchedule == null)
            {
                return NotFound("Doctor's schedule not found.");
            }

            // Generate time slots based on the schedule's start and end times
            var timeSlots = GenerateTimeSlots(doctorSchedule);

            // Check if the schedule record already exists, if not create a new schedule
            var schedule = await context.Schedules
                .FirstOrDefaultAsync(s => s.DoctorId == doctorId);

            if (schedule == null)
            {
                // Create a new schedule for the doctor if it doesn't exist
                schedule = new Schedule
                {
                    DoctorId = doctorId,
                    Start = doctorSchedule.Start,
                    End = doctorSchedule.End
                };
                context.Schedules.Add(schedule);
                await context.SaveChangesAsync();
            }

            // Set the ScheduleId for each time slot
            foreach (var timeSlot in timeSlots)
            {
                timeSlot.ScheduleId = schedule.Id; // Link to the schedule's ID
            }

            // Save the generated time slots to the database
            await context.TimeSlots.AddRangeAsync(timeSlots);
            await context.SaveChangesAsync();

            // Return a response with the created time slots
            return CreatedAtAction(nameof(CreateDoctorTimeSlots), new { doctorId = doctorId }, timeSlots);
        }


        // Helper method to generate time slots from the doctor's schedule
        private List<TimeSlot> GenerateTimeSlots(Schedule schedule)
        {
            var timeSlots = new List<TimeSlot>();
            var startTime = schedule.Start;
            var endTime = schedule.End;

            // Generate 1-hour time slots between the start and end time
            while (startTime < endTime)
            {
                timeSlots.Add(new TimeSlot
                {
                    DoctorId = schedule.DoctorId,
                    Start = startTime,
                    End = startTime.AddHours(1)
                });

                startTime = startTime.AddHours(1);
            }

            return timeSlots;
        }
    }
}