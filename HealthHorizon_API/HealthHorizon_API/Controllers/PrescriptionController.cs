﻿using HealthHorizon_API.Data;
using HealthHorizon_API.Models.PersonTypes;
using Microsoft.AspNetCore.Http;
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

		[HttpGet("{id}")]
		public async Task<ActionResult<Prescription>> GetPrescription(int id)
		{
			var prescription = await context.Prescriptions.Include(p => p.Appointment).FirstOrDefaultAsync(p => p.Id == id);
			if (prescription == null)
			{
				return NotFound();
			}

			return Ok(prescription);
		}

		[HttpPost]
		public async Task<ActionResult> PostPrescription([FromBody] Prescription prescription)
		{
			if (prescription == null)
			{
				return BadRequest();
			}

			await context.Prescriptions.AddAsync(prescription);
			await context.SaveChangesAsync();

			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> UpdatePrescription([FromBody] Prescription prescription)
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

		[HttpDelete]
		public async Task<ActionResult> DeletePrescription(int id)
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
