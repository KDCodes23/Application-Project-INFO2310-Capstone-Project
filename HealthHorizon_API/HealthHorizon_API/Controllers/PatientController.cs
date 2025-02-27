using HealthHorizon_API.Data;
using HealthHorizon_API.Models.PersonTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PatientController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public PatientController(HealthHorizonContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<StaffRole>>> GetAllPatients()
		{
			var patients = await context.Patients.ToListAsync();
			if (patients == null)
			{
				return NotFound();
			}
			return Ok(patients);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<StaffRole>> GetPatient(int id)
		{
			var patient = await context.Patients.FirstOrDefaultAsync(x => x.Id == id);
			if (patient == null)
			{
				return NotFound();
			}
			return Ok(patient);
		}

		[HttpPost]
		public async Task<ActionResult> PostPatient(Patient newPatient)
		{
			await context.Patients.AddAsync(newPatient);
			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> UpdatePatient(Patient newPatient)
		{
			var patient = await context.Patients.FirstOrDefaultAsync(x => x.Id == newPatient.Id);
			if (patient == null)
			{
				return NotFound();
			}
			patient.Name = newPatient.Name;
			patient.DateOfBirth = newPatient.DateOfBirth;
			patient.Email = newPatient.Email;
			patient.PhoneNumber = newPatient.PhoneNumber;
			patient.Address = newPatient.Address;
			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete]
		public async Task<ActionResult> DeletePatient(int id)
		{
			var patient = await context.Patients.FirstOrDefaultAsync(x => x.Id == id);
			if (patient == null)
			{
				return NotFound();
			}
			context.Patients.Remove(patient);
			await context.SaveChangesAsync();
			return Ok();
		}
	}
}
