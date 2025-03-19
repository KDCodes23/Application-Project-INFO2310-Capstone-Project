using HealthHorizon_API.Data;
using HealthHorizon_API.Models.PersonTypes;
using HealthHorizon_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DoctorController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public DoctorController(HealthHorizonContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<StaffRole>>> GetAllDoctors()
		{
			var doctors = await context.Doctors.ToListAsync();
			if (doctors == null)
			{
				return NotFound();
			}
			return Ok(doctors);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<StaffRole>> GetDoctor(int id)
		{
			var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
			if (doctor == null)
			{
				return NotFound();
			}
			return Ok(doctor);
		}

		[HttpPost]
		public async Task<ActionResult> PostDoctor([FromBody] Doctor newDocotor)
		{
			await context.Doctors.AddAsync(newDocotor);
			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> UpdateDoctor([FromBody] Doctor newDoctor)
		{
			var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == newDoctor.Id);
			if (doctor == null)
			{
				return NotFound();
			}
			doctor.FirstName = newDoctor.FirstName;
            doctor.LastName = newDoctor.LastName;
            doctor.Email = newDoctor.Email;
            doctor.PhoneNumber = newDoctor.PhoneNumber;
            doctor.Specialty = newDoctor.Specialty;
			doctor.HospitalName = newDoctor.HospitalName;
            doctor.ProfessionalBio = newDoctor.ProfessionalBio;
            doctor.Password = newDoctor.Password;
            await context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteDoctor(int id)
		{
			var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
			if (doctor == null)
			{
				return NotFound();
			}
			context.Doctors.Remove(doctor);
			await context.SaveChangesAsync();
			return Ok();
		}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Doctor doctor)
        {
            // Validate required fields
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check for duplicate email (across both roles)
            if (await context.Doctors.AnyAsync(d => d.Email == doctor.Email) ||
                await context.Patients.AnyAsync(p => p.Email == doctor.Email))
            {
                return Conflict("Email already registered.");
            }

            // Save doctor
            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            return Ok(new { success = true });
        }
    }
}
