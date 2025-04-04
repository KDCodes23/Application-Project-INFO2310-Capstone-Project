using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
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

			if (appointments is null)
			{
				return NotFound("Appointments Not Found");
			}

			return Ok(appointments);
		}

		[HttpGet("doctor-appointments")]
		public async Task<ActionResult<List<Appointment>>> GetDoctorAppointments([FromQuery] Guid id)
		{
			if (id == Guid.Empty)
			{
				return BadRequest("Id Required");
			}

			var appointments = await context.Appointments.Include(a => a.TimeSlot).Include(a => a.Doctor).Include(a => a.Patient).Where(a => a.DoctorId == id).ToListAsync();
			if (appointments is null)
			{
				return NotFound("Appointments Not Found");
			}

			return Ok(appointments);
		}

		[HttpGet("patient-appointments")]
		public async Task<ActionResult<List<Appointment>>> GetPatientAppointments([FromQuery] Guid id)
		{
			if (id == Guid.Empty)
			{
				return BadRequest("Id Required");
			}

			var appointments = await context.Appointments.Include(a => a.TimeSlot).Include(a => a.Doctor).Include(a => a.Patient).Where(a => a.PatientId == id).ToListAsync();
			if (appointments is null)
			{
				return NotFound("Appointments Not Found");
			}

			return Ok(appointments);
		}

		//[Authorize]
		[HttpGet("get-appointment")]
		public async Task<ActionResult<Appointment>> GetAppointment([FromQuery] Guid id)
		{
			if (id == Guid.Empty)
			{
				return BadRequest("Id Required");
			}

			var appointment = await context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).FirstOrDefaultAsync(a => a.Id == id);
			if (appointment is null)
			{
				return NotFound("Appointment Not Found");
			}

			return Ok(appointment);
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpPost]
		public async Task<ActionResult> PostAppointment([FromBody] Appointment appointment)
		{
			if (appointment == null)
			{
				return BadRequest("Appointment Data Required");
			}

			var timeSlot = await context.TimeSlots
				.FirstOrDefaultAsync(ts => ts.Id == appointment.TimeSlotId);

			if (timeSlot == null)
			{
				return NotFound($"Time Slot with ID {appointment.TimeSlotId} not found");
			}

			if (!timeSlot.IsAvailable)
			{
				return Conflict("Time Slot is already booked.");
			}

			timeSlot.IsAvailable = false;

			context.TimeSlots.Update(timeSlot);
			await context.SaveChangesAsync();

			await context.Appointments.AddAsync(appointment);
			await context.SaveChangesAsync();

			return CreatedAtAction(nameof(PostAppointment), new { id = appointment.Id }, appointment);
		}


		//[Authorize(Roles = "admin, doctor")]
		[HttpPut]
		public async Task<ActionResult> UpdateAppointment([FromBody] Appointment appointment)
		{
			if (appointment is null)
			{
				return BadRequest("Appointment Data Required");
			}

			var appointmentDB = await context.Appointments.FirstOrDefaultAsync(a => a.Id == appointment.Id);
			if (appointmentDB is null)
			{
				return NotFound("Appointment Not Found");
			}

			appointmentDB.Date = appointment.Date;
			appointmentDB.Status = appointment.Status;
			appointmentDB.DoctorId = appointment.DoctorId;
			appointmentDB.PatientId = appointment.PatientId;

			await context.SaveChangesAsync();
			return Ok("Appointment Updated");
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpDelete("delete-appointment")]
		public async Task<ActionResult> DeleteAppointment([FromQuery] Guid id)
		{
			if (id == Guid.Empty)
			{
				return BadRequest("Id Required");
			}

			var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
			if (appointment is null)
			{
				return NotFound("Appointment Not Found");
			}

			context.Appointments.Remove(appointment);
			await context.SaveChangesAsync();

			return Ok("Appointment Deleted");
		}
	}
}