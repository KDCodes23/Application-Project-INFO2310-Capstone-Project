using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
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

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		//[Authorize(Roles = "staff")]
		//[Authorize(Roles = "patient")]
		[HttpGet("{id:int}")]
		public async Task<ActionResult<Appointment>> GetAppointment([FromQuery] int id)
		{
			var appointment = await context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).FirstOrDefaultAsync(a => a.Id == id);
			if (appointment == null)
			{
				return NotFound();
			}

			return Ok(appointment);
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		[HttpPost("add-appointment")]
		public async Task<ActionResult> PostAppointment([FromBody] Appointment appointment)
		{
			await context.Appointments.AddAsync(appointment);
			await context.SaveChangesAsync();
			return Ok();
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		[HttpPut("update-appointment")]
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
			return Ok();
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		[HttpDelete("remove-{id:int}")]
		public async Task<ActionResult> DeleteAppointment([FromQuery] int id)
		{
			var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
			if (appointment == null)
			{
				return NotFound();
			}

			context.Appointments.Remove(appointment);
			await context.SaveChangesAsync();

			return Ok();
		}
	}
}
