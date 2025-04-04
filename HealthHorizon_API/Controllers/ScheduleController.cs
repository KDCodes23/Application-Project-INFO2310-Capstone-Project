using HealthHorizon_API.Data;
using HealthHorizon_API.Models.DTOs;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScheduleController : ControllerBase
	{
		private readonly HealthHorizonContext context;

        public ScheduleController(HealthHorizonContext context)
        {
            this.context = context;
        }


        [HttpPost("doctor/{doctorId}")]
        public async Task<IActionResult> AddSchedule(Guid doctorId, [FromBody] ScheduleDTO scheduleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid schedule data.");
            }

            if (scheduleDTO == null)
                return BadRequest("Schedule data is required.");

            if (scheduleDTO.Start >= scheduleDTO.End)
                return BadRequest("Invalid schedule data.");

            var doctor = await context.Doctors.FindAsync(doctorId);
            if (doctor == null)
                return NotFound("Doctor not found.");

            var newSchedule = new Schedule
            {
                Date = scheduleDTO.Date,
                Start = scheduleDTO.Start,
                End = scheduleDTO.End,
                DoctorId = doctorId
            };

            context.Schedules.Add(newSchedule);
            await context.SaveChangesAsync();

            // Removed code for generating time slots

            return CreatedAtAction(nameof(GetScheduleById), new { id = newSchedule.Id }, newSchedule);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetScheduleById(Guid id)
        {
            var schedule = await context.Schedules
                .Include(s => s.TimeSlots)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (schedule == null)
                return NotFound("Schedule not found.");

            return Ok(schedule);
        }

        private List<TimeSlot> GenerateTimeSlots(Schedule schedule)
        {
            var timeSlots = new List<TimeSlot>();
            var currentTime = schedule.Start;

            while (currentTime < schedule.End)
            {
                var endTime = currentTime.Add(TimeSpan.FromMinutes(30)); // Assuming 30-minute slots
                if (endTime > schedule.End)
                    break;

                timeSlots.Add(new TimeSlot
                {
                    Start = currentTime,
                    End = endTime,
                    ScheduleId = schedule.Id,
                    IsAvailable = true
                });

                currentTime = endTime;
            }

            return timeSlots;
        }
    }
}
