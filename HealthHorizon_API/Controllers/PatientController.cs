using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
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
        public async Task<ActionResult<StaffRole>> GetPatient(int id)
        {
            var patient = await context.Patients.Include(p => p.Address).FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult> PostPatient([FromBody] Patient newPatient)
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

		[HttpPut]
        public async Task<ActionResult> UpdatePatient([FromBody] Patient newPatient)
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
