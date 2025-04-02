﻿using HealthHorizon_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthHorizon_API.Models.Entities;
using HealthHorizon_API.Models.UtilityModels;

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

		//[Authorize(Roles = "admin, doctor, staff")]
		[HttpGet]
		public async Task<ActionResult<List<StaffRole>>> GetAllDoctors()
		{
			var doctors = await context.Doctors.ToListAsync();
			if (doctors is null)
			{
				return NotFound("Doctors Not Found");
			}
			return Ok(doctors);
		}

		//[Authorize(Roles = "admin, doctor, staff")]
		[HttpGet("get-doctor")]
		public async Task<ActionResult<StaffRole>> GetDoctor([FromBody] IdRequest request)
		{
			if (request is null || request.Id == Guid.Empty)
			{
				return BadRequest("Id Required");
			}

			var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == request.Id);
			if (doctor is null)
			{
				return NotFound("Doctor Not Found");
			}
			return Ok(doctor);
		}

		//[Authorize(Roles = "admin")]
		[HttpPost]
		public async Task<ActionResult> PostDoctor([FromBody] Doctor newDocotor)
		{
			if (newDocotor is null)
			{
				return BadRequest("Doctor Data Required");
			}

			await context.Doctors.AddAsync(newDocotor);
			await context.SaveChangesAsync();
			return Created();
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpPut]
		public async Task<ActionResult> UpdateDoctor([FromBody] Doctor newDoctor)
		{
			if (newDoctor is null)
			{
				return BadRequest("Doctor Data Required");	
			}

			var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == newDoctor.Id);
			if (doctor is null)
			{
				return NotFound("Doctor Not Found");
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

			return Ok("Doctor Updated");
		}

		//[Authorize(Roles = "admin")]
		[HttpDelete("delete-doctor")]
		public async Task<ActionResult> DeleteDoctor([FromBody] IdRequest request)
		{
			if (request is null || request.Id == Guid.Empty)
			{
				return BadRequest("Id Required");
			}

			var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == request.Id);
			if (doctor is null)
			{
				return NotFound("Doctor Not Found");
			}
			context.Doctors.Remove(doctor);
			await context.SaveChangesAsync();
			return Ok("Doctor Deleted");
		}
    }
}
