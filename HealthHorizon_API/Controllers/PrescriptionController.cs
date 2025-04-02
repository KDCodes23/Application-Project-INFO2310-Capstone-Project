using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using HealthHorizon_API.Models.UtilityModels;
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
				return NotFound("Prescriptions Not Found");
			}

			return Ok(prescriptions);
		}

		[HttpGet]

		//[Authorize]
		[HttpGet("get-prescription")]
		public async Task<ActionResult<Prescription>> GetPrescription([FromBody] IdRequest request)
		{
			var prescription = await context.Prescriptions.Include(p => p.Appointment).FirstOrDefaultAsync(p => p.Id == request.Id);
			if (prescription == null)
			{
				return NotFound("Prescription Not Found");
			}

			return Ok(prescription);
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpPost]
		public async Task<ActionResult> PostPrescription([FromBody] Prescription prescription)
		{
			if (prescription == null)
			{
				return BadRequest("Prescription Data Required");
			}

			await context.Prescriptions.AddAsync(prescription);
			await context.SaveChangesAsync();

			return Created();
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpPut]
		public async Task<ActionResult> UpdatePrescription([FromBody] Prescription prescription)
		{
			var prescriptionDB = await context.Prescriptions.FirstOrDefaultAsync(p => p.Id == prescription.Id);
			if (prescriptionDB == null)
			{
				return NotFound("Prescription Not Found");
			}

			prescriptionDB.MedicationName = prescription.MedicationName;
			prescriptionDB.Dosage = prescription.Dosage;
			prescriptionDB.Instructions = prescription.Instructions;
			prescriptionDB.AppointmentId = prescription.AppointmentId;
			await context.SaveChangesAsync();

			return Ok("Prescription Updated");
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpDelete]
		public async Task<ActionResult> DeletePrescription([FromBody] IdRequest request)
		{
			var prescription = await context.Prescriptions.FirstOrDefaultAsync(p => p.Id == request.Id);
			if (prescription == null)
			{
				return NotFound("Prescription Not Found");
			}

			context.Prescriptions.Remove(prescription);
			await context.SaveChangesAsync();

			return Ok("Prescription Deleted");
		}
	}
}
