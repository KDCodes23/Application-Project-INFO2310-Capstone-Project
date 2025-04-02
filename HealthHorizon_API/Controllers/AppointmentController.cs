﻿using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using HealthHorizon_API.Models.UtilityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppointmentController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public AppointmentController(HealthHorizonContext context)
		{
			this.context = context;
		}

		//[Authorize(Roles = "admin")]
		[HttpGet]
		public async Task<ActionResult<List<Appointment>>> GetAllAppointments()
		{
			var appointments = await context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).ToListAsync();

			if (appointments == null)
			{
				return NotFound();
			}

			return Ok(appointments);
		}

		//[Authorize]
		[HttpGet("get-appointment")]
		public async Task<ActionResult<Appointment>> GetAppointment([FromBody] IdRequest request)
		{
			var appointment = await context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).FirstOrDefaultAsync(a => a.Id == request.Id);
			if (appointment == null)
			{
				return NotFound();
			}

			return Ok(appointment);
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpPost]
		public async Task<ActionResult> PostAppointment([FromBody] Appointment appointment)
		{
			await context.Appointments.AddAsync(appointment);
			await context.SaveChangesAsync();

			//return Ok();
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

		//[Authorize(Roles = "admin, doctor")]
		[HttpPut]
		public async Task<ActionResult> UpdateAppointment([FromBody] Appointment appointment)
		{
			var appointmentDB = await context.Appointments.FirstOrDefaultAsync(a => a.Id == appointment.Id);
			if (appointmentDB == null)
			{
				return NotFound();
			}

			appointmentDB.Date = appointment.Date;
			appointmentDB.Status = appointment.Status;
			appointmentDB.DoctorId = appointment.DoctorId;
			appointmentDB.PatientId = appointment.PatientId;

			await context.SaveChangesAsync();
            return NoContent();
        }

		//[Authorize(Roles = "admin, doctor")]
		[HttpDelete("delete-appointment")]
		public async Task<ActionResult> DeleteAppointment([FromBody] IdRequest request)
		{
			var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.Id == request.Id);
			if (appointment == null)
			{
				return NotFound();
			}

			context.Appointments.Remove(appointment);
			await context.SaveChangesAsync();

            return NoContent();
        }
	}
}
