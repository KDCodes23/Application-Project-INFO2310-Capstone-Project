﻿using HealthHorizon_API.Data;
using HealthHorizon_API.Models.DTOs;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScheduleController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public ScheduleController(HealthHorizonContext context) => this.context = context;

		[HttpGet]
		public async Task<ActionResult<List<Schedule>>> GetAllSchedules()
		{
			var schedules = await context.Schedules.Include(s => s.TimeSlots).ToListAsync();
			if (schedules is null) return NotFound("No Schedules Found");

			return Ok(schedules);
		}

		[HttpGet("Doctor")]
		public async Task<ActionResult<List<Schedule>>> GetDoctorSchedules([FromQuery] Guid id)
		{
			if (id == Guid.Empty) return BadRequest("Id Required");

			var schedules = await context.Schedules.Include(s => s.TimeSlots).Where(s => s.DoctorId == id).ToListAsync();
			if (schedules is null) return NotFound("No Schedules Found For That Doctor.");

			return Ok(schedules);
		}

		[HttpPost]
		public async Task<ActionResult> PostSchedule([FromBody] ScheduleDTO scheduleDTO)
		{
			if (scheduleDTO is null) return BadRequest("Data Is Null");

			var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.Id == scheduleDTO.DoctorId);

			if (scheduleDTO.Id == Guid.Empty || scheduleDTO.Date == DateOnly.MinValue || scheduleDTO.Start >= scheduleDTO.End || doctor is null)
				return BadRequest("Schedule Data Required");

			Schedule newSchedule = new Schedule
			{
				Date = scheduleDTO.Date,
				Start = scheduleDTO.Start,
				End = scheduleDTO.End,
				DoctorId = scheduleDTO.DoctorId
			};

			if ((int)(scheduleDTO.End - scheduleDTO.Start).TotalHours < 1) 
				return BadRequest("Invalid Time Span");

			await context.Schedules.AddAsync(newSchedule);
			await context.SaveChangesAsync();
			await CreateTimeSlots(newSchedule);

			return Created();
		}

		[HttpPut("change-doctor")]
		public async Task<ActionResult> UpdateScheduleDoctor([FromBody] ScheduleDTO scheduleDTO)
		{
			if (scheduleDTO is null) return BadRequest("Data Is Null");

			var schedule = await context.Schedules.FirstOrDefaultAsync(s => s.Id == scheduleDTO.Id);
			if (schedule is null) return NotFound("Schedule Does Not Exist");

			var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.Id == scheduleDTO.DoctorId);

			if (scheduleDTO.Id == Guid.Empty || scheduleDTO.Date == DateOnly.MinValue || scheduleDTO.Start >= scheduleDTO.End || doctor is null)
				return BadRequest("Invalid Schedule Data");

			schedule.DoctorId = scheduleDTO.Id;
			await context.SaveChangesAsync();

			return Ok("Scheduled Docotr Updated");
		}

		[HttpPut("change-date")]
		public async Task<ActionResult> UpdateScheduleDate([FromBody] ScheduleDTO scheduleDTO)
		{
			if (scheduleDTO is null) return BadRequest("Data Is Null");

			var schedule = await context.Schedules.Include(s => s.TimeSlots).FirstOrDefaultAsync(s => s.Id == scheduleDTO.Id);
			if (schedule is null) return NotFound("Schedule Does Not Exist");

			var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.Id == scheduleDTO.DoctorId);

			if (scheduleDTO.Id == Guid.Empty || scheduleDTO.Date == DateOnly.MinValue || scheduleDTO.Start >= scheduleDTO.End || doctor is null)
				return BadRequest("Schedule Data Required");

			schedule.Date = scheduleDTO.Date;
			schedule.Start = scheduleDTO.Start;
			schedule.End = scheduleDTO.End;

			context.TimeSlots.RemoveRange(schedule.TimeSlots);

			await CreateTimeSlots(schedule);
			await context.SaveChangesAsync();

			return Ok("Schedule Date Updated");
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteSchedule([FromQuery] Guid id)
		{
			if (id == Guid.Empty) return BadRequest("Id Required");

			var schedule = await context.Schedules.Include(s => s.TimeSlots).FirstOrDefaultAsync(s => s.Id == id);
			if (schedule is null) return NotFound("Schedule Not Found");

			context.TimeSlots.RemoveRange(schedule.TimeSlots);
			context.Schedules.Remove(schedule);
			await context.SaveChangesAsync();

			return Ok("Schedule Deleted");
		}

		private async Task CreateTimeSlots(Schedule schedule)
		{
			int slots = (int)(schedule.End - schedule.Start).TotalHours;
			int start = (int)schedule.Start.ToTimeSpan().TotalHours;
			int end = start + 1;

			for (int i = 0; i < slots; i++)
			{
				await context.TimeSlots.AddAsync(new TimeSlot 
				{
					Start = new TimeOnly(start, 0), 
					End = new TimeOnly(end, 0),
					ScheduleId = schedule.Id
				});
				start++;
				end++;
			}
		}
	}
}
