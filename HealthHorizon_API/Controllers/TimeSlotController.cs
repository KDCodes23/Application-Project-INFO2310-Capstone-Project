using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Http;
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

		[HttpGet("{id}")]
		public async Task<ActionResult<List<TimeSlot>>> GetDoctorTimeSlots([FromBody] int id)
		{
			var timeSlots = await context.TimeSlots.Where(ts => ts.DoctorId == id).ToListAsync();
			if (timeSlots == null)
			{
				return NotFound();
			}

			return Ok(timeSlots);
		}

		[HttpPost]
		public async Task<ActionResult> PostTimeSlot([FromBody] TimeSlot timeSlot)
		{
			if (timeSlot == null)
			{
				return BadRequest();
			}

			await context.TimeSlots.AddAsync(timeSlot);
			await context.SaveChangesAsync();

			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> UpdateTimeSlot([FromBody] TimeSlot timeSlot)
		{
			if (timeSlot == null)
			{
				return BadRequest();
			}

			var timeSlotDB = await context.TimeSlots.FirstOrDefaultAsync(ts => ts.Id == timeSlot.Id);
			if (timeSlotDB == null)
			{
				return NotFound();
			}

			timeSlotDB.Start = timeSlot.Start;
			timeSlotDB.End = timeSlot.End;
			timeSlotDB.DoctorId = timeSlot.DoctorId;
			await context.SaveChangesAsync();

			return Ok();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteTimeSlot([FromBody] int id)
		{
			var timeSlot = await context.TimeSlots.FirstOrDefaultAsync(ts => ts.Id == id);
			if (timeSlot == null)
			{
				return NotFound();
			}

			context.TimeSlots.Remove(timeSlot);
			await context.SaveChangesAsync();

			return Ok();
		}
	}
}
