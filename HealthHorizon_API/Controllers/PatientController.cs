﻿using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using HealthHorizon_API.Models.UtilityModels;
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

		//[Authorize(Roles = "admin, doctor, staff")]
		[HttpGet]
		public async Task<ActionResult<StaffRole>> GetAllPatients()
		{
			var patients = await context.Patients.Include(p => p.Address).ToListAsync();
			if (patients is null)
			{
				return NotFound("Patients Not Found");
			}

			return Ok(patients);
		}

		//[Authorize]
		[HttpGet("get-patient")]
        public async Task<ActionResult<StaffRole>> GetPatient([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id Required");
            }

            var patient = await context.Patients.Include(p => p.Address).FirstOrDefaultAsync(x => x.Id == id);
            if (patient is null)
            {
                return NotFound("Patient Not Found");
            }

            return Ok(patient);
        }

		//[Authorize(Roles = "admin, patient")]
		[HttpPut]
        public async Task<ActionResult> UpdatePatient([FromBody] Patient newPatient)
        {
			if (newPatient is null)
			{
				return BadRequest("Patient Data Required");
			}

			var patient = await context.Patients.FirstOrDefaultAsync(x => x.Id == newPatient.Id);
            if (patient is null)
            {
                return NotFound("Patient Not Found");
            }
            patient.FirstName = newPatient.FirstName;
            patient.LastName = newPatient.LastName;
            patient.DateOfBirth = newPatient.DateOfBirth;
            patient.PhoneNumber = newPatient.PhoneNumber;
            patient.AddressId = newPatient.AddressId;
            patient.Gender = newPatient.Gender;

            await context.SaveChangesAsync();
            return Ok("Patient Updated");
        }

		//[Authorize(Roles = "admin")]
		[HttpDelete("delete-patient")]
        public async Task<ActionResult> DeletePatient([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id Required");
            }

            var patient = await context.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (patient is null)
            {
                return NotFound("Patient Not Found");
            }
            context.Patients.Remove(patient);
            await context.SaveChangesAsync();
            return Ok("Patient Deleted");
        }
    }
}
