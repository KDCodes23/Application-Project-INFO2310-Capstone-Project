using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
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

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		//[Authorize(Roles = "staff")]
		[HttpGet]
		public async Task<ActionResult<StaffRole>> GetAllPatients()
		{
			var patients = await context.Patients.Include(p => p.Address).ToListAsync();
			if (patients == null)
			{
				return NotFound();
			}
			return Ok(patients);
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		//[Authorize(Roles = "staff")]
		//[Authorize(Roles = "patient")]
		[HttpGet("{id:int}")]
        public async Task<ActionResult<StaffRole>> GetPatient([FromQuery] int id)
        {
            var patient = await context.Patients.Include(p => p.Address).FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		//[Authorize(Roles = "staff")]
		[HttpPost]
        public async Task<ActionResult> PostPatient([FromForm] Patient newPatient)
        {
            newPatient.Address = new Address
            {
                Street = newPatient.Address.Street,
                City = newPatient.Address.City,
                ProvinceOrState = newPatient.Address.ProvinceOrState,
                Country = newPatient.Address.Country,
                PostalCode = newPatient.Address.PostalCode
            };

		
            await context.Patients.AddAsync(newPatient);
            await context.SaveChangesAsync();
            return Ok();
        }

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "patient")]
		[HttpPut]
        public async Task<ActionResult> UpdatePatient([FromForm] Patient newPatient)
        {
            var patient = await context.Patients.FirstOrDefaultAsync(x => x.Id == newPatient.Id);
            if (patient == null)
            {
                return NotFound();
            }
            patient.FirstName = newPatient.FirstName;
            patient.LastName = newPatient.LastName;
            patient.DateOfBirth = newPatient.DateOfBirth;
            patient.PhoneNumber = newPatient.PhoneNumber;
            patient.AddressId = newPatient.AddressId;
            patient.Gender = newPatient.Gender;

            await context.SaveChangesAsync();
            return Ok();
        }

		//[Authorize(Roles = "admin")]
		[HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePatient([FromQuery] int id)
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
