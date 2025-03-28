using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PrescriptionController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public PrescriptionController(HealthHorizonContext context)
		{
			this.context = context;
		}

		//[Authorize(Roles = "admin")]
		[HttpGet]
		public async Task<ActionResult<List<Prescription>>> GetAllPrescriptions()
		{
			var prescriptions = await context.Prescriptions.Include(p => p.Appointment).ToListAsync();
			if (prescriptions == null)
			{
				return NotFound();
			}

			return Ok(prescriptions);
		}

		//[Authorize]
		[HttpGet("{id:int}")]
		public async Task<ActionResult<Prescription>> GetPrescription([FromQuery] int id)
		{
			var prescription = await context.Prescriptions.Include(p => p.Appointment).FirstOrDefaultAsync(p => p.Id == id);
			if (prescription == null)
			{
				return NotFound();
			}

			return Ok(prescription);
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		[HttpPost]
		public async Task<ActionResult> PostPrescription([FromForm] Prescription prescription)
		{
			if (prescription == null)
			{
				return BadRequest();
			}

			await context.Prescriptions.AddAsync(prescription);
			await context.SaveChangesAsync();

			return Ok();
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		[HttpPut]
		public async Task<ActionResult> UpdatePrescription([FromForm] Prescription prescription)
		{
			var prescriptionDB = await context.Prescriptions.FirstOrDefaultAsync(p => p.Id == prescription.Id);
			if (prescriptionDB == null)
			{
				return NotFound();
			}

			prescriptionDB.MedicationName = prescription.MedicationName;
			prescriptionDB.Dosage = prescription.Dosage;
			prescriptionDB.Instructions = prescription.Instructions;
			prescriptionDB.AppointmentId = prescription.AppointmentId;
			await context.SaveChangesAsync();

			return Ok();
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		[HttpDelete("{id:int}")]
		public async Task<ActionResult> DeletePrescription([FromQuery] int id)
		{
			var prescription = await context.Prescriptions.FirstOrDefaultAsync(p => p.Id == id);
			if (prescription == null)
			{
				return NotFound();
			}

			context.Prescriptions.Remove(prescription);
			await context.SaveChangesAsync();

			return Ok();
		}
	}
}
