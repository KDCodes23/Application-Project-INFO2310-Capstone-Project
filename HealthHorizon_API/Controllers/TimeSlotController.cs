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

		public TimeSlotController(HealthHorizonContext context) => this.context = context;

		[HttpGet]
		public async Task<ActionResult<List<TimeSlot>>> GetAllTimeSlots()
		{
			var timeSlots = await context.TimeSlots.ToListAsync();
			if (timeSlots is null) return NotFound("Time Slots Not Found");

			return Ok(timeSlots);
		}

		[HttpGet("patient-slots")]
		public async Task<ActionResult<List<TimeSlot>>> GetPatientTimeSlots([FromQuery] Guid id)
		{
			if (id == Guid.Empty) return BadRequest("Patient Id Required");

			var timeSlots = await context.TimeSlots.Include(ts => ts.Schedule).Where(ts => ts.PatientId == id).ToListAsync();
			if (timeSlots is null) return NotFound("Time Slots Not Found");

			return Ok(timeSlots);
		}

		[HttpGet]
		public async Task<ActionResult<TimeSlot>> GetTimeSlot([FromQuery] Guid id)
		{
			if (id == Guid.Empty) return BadRequest("Id Required");

			var timeSlot = await context.TimeSlots.Include(ts => ts.Schedule).FirstOrDefaultAsync(ts => ts.Id == id);
			if (timeSlot is null) return NotFound("Time Slot Not Found");

			return Ok(timeSlot);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateTimeSlot([FromBody] TimeSlotDTO newTimeSlot)
		{
			if (newTimeSlot is null) return BadRequest("Time Slot Data Required");

			var timeSlotDB = await context.TimeSlots.FindAsync(newTimeSlot.Id);
			if (timeSlotDB is null) return NotFound("Time Slot Not Found");

			timeSlotDB.Start = newTimeSlot.Start;
			timeSlotDB.End = newTimeSlot.End;
			timeSlotDB.IsAvailible = newTimeSlot.IsAvailible;
			timeSlotDB.PatientId = newTimeSlot.PatientId;
			timeSlotDB.ScheduleId = newTimeSlot.ScheduleId;

			await context.SaveChangesAsync();

			return Created();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteTimeSlot([FromQuery] Guid id)
		{
			if (id == Guid.Empty) return BadRequest("Id Required");

			var timeSlot = await context.TimeSlots.FindAsync(id);
			if (timeSlot is null) return NotFound("Time Slot Not Found");

			context.TimeSlots.Remove(timeSlot);
			await context.SaveChangesAsync();

			return Ok("TimeSlot Deleted");
		}
	}
}