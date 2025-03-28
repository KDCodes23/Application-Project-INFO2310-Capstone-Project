﻿using HealthHorizon_API.Data;
using HealthHorizon_API.Interfaces;
using HealthHorizon_API.Models.Entities;
using HealthHorizon_API.Models.Identities;
using HealthHorizon_API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizeController : ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly IJwtTokenService jwtTokenService;
		private readonly HealthHorizonContext context;

		public AuthorizeController(HealthHorizonContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			jwtTokenService = new JwtTokenService(config, userManager);
			this.context = context;
		}

		//[Authorize(Roles = "admin")]
		[HttpPost("register-doctor")]
		public async Task<ActionResult> RegisterDoctor([FromBody] Register request)
		{
			if (request.Doctor == null)
			{
				return BadRequest();
			}

			var existingUser = await userManager.FindByEmailAsync(request.Email);
			if (existingUser != null)
			{
				return BadRequest();
			}

			var user = new IdentityUser
			{
				UserName = request.UserName,
				Email = request.Email,
				EmailConfirmed = true
				
			};

			var result = await userManager.CreateAsync(user, request.Password);
			if (!result.Succeeded)
			{
				return BadRequest();
			}

			await userManager.AddToRoleAsync(user, "doctor");

			var doctor = new Doctor
			{
				FirstName = request.Doctor.FirstName,
				LastName = request.Doctor.LastName,
				Specialization = request.Doctor.Specialization,
				PhoneNumber = request.Doctor.PhoneNumber,
				UserId = user.Id,
				User = user
			};

			await context.Doctors.AddAsync(doctor);
			await context.SaveChangesAsync();

			return Ok();
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		//[Authorize(Roles = "staff")]
		[HttpPost("register-patient")]
		public async Task<ActionResult> RegisterPatient([FromBody] Register request)
		{
			if (request.Patient == null)
			{
				return BadRequest();
			}

			var existingUser = await userManager.FindByEmailAsync(request.Email);
			if (existingUser != null)
			{
				return BadRequest();
			}

			var user = new IdentityUser
			{
				UserName = request.UserName,
				Email = request.Email,
				EmailConfirmed = true

			};

			var result = await userManager.CreateAsync(user, request.Password);
			if (!result.Succeeded)
			{
				return BadRequest();
			}

			await userManager.AddToRoleAsync(user, "patient");

			var patient = new Patient
			{
				FirstName = request.Patient.FirstName,
				DateOfBirth = request.Patient.DateOfBirth,
				PhoneNumber = request.Patient.PhoneNumber,
				AddressId = request.Patient.AddressId,
				UserId = user.Id,
				User = user
			};

			await context.Patients.AddAsync(patient);
			await context.SaveChangesAsync();

			return Ok();
		}

		//[Authorize(Roles = "admin")]
		[HttpPost("register-staff")]
		public async Task<ActionResult> RegisterStaff([FromBody] Register request)
		{
			if (request.Staff == null)
			{
				return BadRequest();
			}

			var existingUser = await userManager.FindByEmailAsync(request.Email);
			if (existingUser != null)
			{
				return BadRequest();
			}

			var user = new IdentityUser
			{
				UserName = request.UserName,
				Email = request.Email,
				EmailConfirmed = true

			};

			var result = await userManager.CreateAsync(user, request.Password);
			if (!result.Succeeded)
			{
				return BadRequest();
			}

			await userManager.AddToRoleAsync(user, "staff");

			var staff = new Staff
			{
				Name= request.Staff.Name,
				PhoneNumber= request.Staff.PhoneNumber,
				RoleId = request.Staff.RoleId,
				UserId = user.Id,
				User = user
			};

			await context.StaffMembers.AddAsync(staff);
			await context.SaveChangesAsync();

			return Ok();
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login([FromBody] Login request)
		{
			var user = await userManager.FindByEmailAsync(request.Email);
			if (user == null)
			{
				return NotFound();
			}
			var result = await signInManager.PasswordSignInAsync(user, request.Password, false, false);
			if (!result.Succeeded)
			{
				return Unauthorized();
			}
			var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
			var token = await jwtTokenService.GenerateJwtTokenAsync(user);
			int id = 0;

			if (role != null)
			{
				switch (role)
				{
					case "doctor":
						var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.UserId == user.Id);
						id = (doctor != null) ? doctor.Id : 0;
						break;
					case "staff":
						var staff = await context.StaffMembers.FirstOrDefaultAsync(s => s.UserId == user.Id);
						id = (staff != null) ? staff.Id : 0;
						break;
					case "patient":
						var patient = await context.Patients.FirstOrDefaultAsync(p => p.UserId == user.Id);
						id = (patient != null) ? patient.Id : 0;
						break;
				}
			}
			else
			{
				role = string.Empty;
			}

			return Ok(new
			{
				Token = token,
				Role = role,
				Id = id
			});
		}
	}
}
