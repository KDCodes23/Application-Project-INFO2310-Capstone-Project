using HealthHorizon_API.Data;
using HealthHorizon_API.Models;
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
            patient.Email = newPatient.Email;
            patient.PhoneNumber = newPatient.PhoneNumber;
            patient.Address = newPatient.Address;
            patient.Gender = newPatient.Gender;
            patient.MedicalConditions = newPatient.MedicalConditions;
            patient.Password = newPatient.Password;

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


        // Controllers/PatientController.cs
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Patient patient)
        {
            // Validate model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check for duplicate email
            if (await context.Patients.AnyAsync(p => p.Email == patient.Email))
                return Conflict("Email already exists.");

            // Create Address entity
            var address = new Address
            {
                Street = patient.Address.Street,
                City = patient.Address.City,
                ProvinceOrState = patient.Address.ProvinceOrState,
                Country = patient.Address.Country,
                PostalCode = patient.Address.PostalCode
            };

            // Link Address to Patient
            patient.Address = address;
            context.Addresses.Add(address); // Explicitly add address
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            return Ok(new { success = true });
        }
    }
}
