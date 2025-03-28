using HealthHorizon_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Authorization;

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

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		//[Authorize(Roles = "staff")]
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

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		//[Authorize(Roles = "staff")]
		[HttpGet("{id:int}")]
		public async Task<ActionResult<StaffRole>> GetDoctor([FromQuery] int id)
		{
			var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
			if (doctor == null)
			{
				return NotFound();
			}
			return Ok(doctor);
		}

		//[Authorize(Roles = "admin")]
		[HttpPost]
		public async Task<ActionResult> PostDoctor([FromBody] Doctor newDocotor)
		{
			await context.Doctors.AddAsync(newDocotor);
			await context.SaveChangesAsync();
			return Ok();
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
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
			doctor.Specialization = newDoctor.Specialization;
			doctor.User.Email = newDoctor.User.Email;
			doctor.PhoneNumber = newDoctor.PhoneNumber;
            doctor.Specialization = newDoctor.Specialization;
			doctor.HospitalName = newDoctor.HospitalName;
            doctor.ProfessionalBio = newDoctor.ProfessionalBio;

            await context.SaveChangesAsync();

			return Ok();
		}

		//[Authorize(Roles = "admin")]
		[HttpDelete("{id:int}")]
		public async Task<ActionResult> DeleteDoctor([FromQuery] int id)
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
    }
}
