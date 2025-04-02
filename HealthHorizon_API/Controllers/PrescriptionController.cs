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
			if (prescriptions is null)
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
			if (request is null || request.Id == Guid.Empty)
			{
				return BadRequest("Id Required");
			}

			var prescription = await context.Prescriptions.Include(p => p.Appointment).FirstOrDefaultAsync(p => p.Id == request.Id);
			if (prescription is null)
			{
				return NotFound("Prescription Not Found");
			}

			return Ok(prescription);
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpPost]
		public async Task<ActionResult> PostPrescription([FromBody] Prescription newPrescription)
		{
			if (newPrescription is null)
			{
				return BadRequest("Prescription Data Required");
			}

			await context.Prescriptions.AddAsync(newPrescription);
			await context.SaveChangesAsync();

			return Created();
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpPut]
		public async Task<ActionResult> UpdatePrescription([FromBody] Prescription newPrescription)
		{
			if (newPrescription is null)
			{
				return BadRequest("Prescription Data Required");
			}

			var prescriptionDB = await context.Prescriptions.FirstOrDefaultAsync(p => p.Id == newPrescription.Id);
			if (prescriptionDB is null)
			{
				return NotFound("Prescription Not Found");
			}

			prescriptionDB.MedicationName = newPrescription.MedicationName;
			prescriptionDB.Dosage = newPrescription.Dosage;
			prescriptionDB.Instructions = newPrescription.Instructions;
			prescriptionDB.AppointmentId = newPrescription.AppointmentId;
			await context.SaveChangesAsync();

			return Ok("Prescription Updated");
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpDelete]
		public async Task<ActionResult> DeletePrescription([FromBody] IdRequest request)
		{
			if (request is null || request.Id == Guid.Empty)
			{
				return BadRequest("Id Required");
			}

			var prescription = await context.Prescriptions.FirstOrDefaultAsync(p => p.Id == request.Id);
			if (prescription is null)
			{
				return NotFound("Prescription Not Found");
			}

			context.Prescriptions.Remove(prescription);
			await context.SaveChangesAsync();

			return Ok("Prescription Deleted");
		}
	}
}
