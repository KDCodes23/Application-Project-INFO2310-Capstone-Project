using HealthHorizon_API.Data;
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
			if (request.Doctor is null) return BadRequest();

			try
			{
                if (!ModelState.IsValid) return BadRequest(ModelState);
                    
                var existingUser = await userManager.FindByEmailAsync(request.Email);
                if (existingUser != null) return BadRequest("Email already registered!");

                var user = new IdentityUser
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    EmailConfirmed = true

                };

                var result = await userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded) return BadRequest();

                await userManager.AddToRoleAsync(user, "doctor");

                var doctor = new Doctor
                {
                    FirstName = request.Doctor.FirstName,
                    LastName = request.Doctor.LastName,
                    Specialization = request.Doctor.Specialization,
                    PhoneNumber = request.Doctor.PhoneNumber,
                    HospitalName = request.Doctor.HospitalName,
                    ProfessionalBio = request.Doctor.ProfessionalBio,
					UserId = user.Id
                };

                await context.Doctors.AddAsync(doctor);
                await context.SaveChangesAsync();

                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while saving patient data");
            }
        }

		//[Authorize(Roles = "admin, doctor, staff")]
		[HttpPost("register-patient")]
		public async Task<ActionResult> RegisterPatient([FromBody] Register request)
		{
			if (request.Patient is null) return BadRequest();

			try
			{
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingUser = await userManager.FindByEmailAsync(request.Email);
				if (existingUser != null) return BadRequest("Email already registered!");

				var user = new IdentityUser
				{
					UserName = request.UserName,
					Email = request.Email,
					EmailConfirmed = true
				};

				var result = await userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(new { message = "User creation failed", errors = result.Errors });
                }
				
                await userManager.AddToRoleAsync(user, "patient");

				Address address = new Address
				{
					Street = request.Patient.Address.Street,
					City = request.Patient.Address.City,
					ProvinceOrState = request.Patient.Address.ProvinceOrState,
					Country = request.Patient.Address.Country,
					PostalCode = request.Patient.Address.PostalCode
				};

				await context.Addresses.AddAsync(address);
				await context.SaveChangesAsync();

				Patient patient = new Patient
				{
					FirstName = request.Patient.FirstName,
                    LastName = request.Patient.LastName,
                    DateOfBirth = request.Patient.DateOfBirth,
					PhoneNumber = request.Patient.PhoneNumber,
					AddressId = address.Id,
					Gender = request.Patient.Gender,
					UserId = user.Id
				};

				await context.Patients.AddAsync(patient);
				await context.SaveChangesAsync();

				return Created();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }


		//[Authorize(Roles = "admin")]
		[HttpPost("register-staff")]
		public async Task<ActionResult> RegisterStaff([FromBody] Register request)
		{
			if (request.Staff is null) return BadRequest();

			var existingUser = await userManager.FindByEmailAsync(request.Email);
			if (existingUser != null) return BadRequest();

			var user = new IdentityUser
			{
				UserName = request.UserName,
				Email = request.Email,
				EmailConfirmed = true

			};

			var result = await userManager.CreateAsync(user, request.Password);
			if (!result.Succeeded) return BadRequest();

			await userManager.AddToRoleAsync(user, "staff");

			var staff = new Staff
			{
				Name= request.Staff.Name,
				PhoneNumber= request.Staff.PhoneNumber,
				RoleId = request.Staff.RoleId,
				UserId = user.Id
			};

			await context.StaffMembers.AddAsync(staff);
			await context.SaveChangesAsync();

			return Created();
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login([FromBody] Login request)
		{
			try
			{
				Guid id = Guid.Empty;
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user is null) return NotFound("User Not Found");

                var result = await signInManager.PasswordSignInAsync(user, request.Password, false, false);
                if (!result.Succeeded) return Unauthorized();

                var token = jwtTokenService.GenerateJwtTokenAsync(user);
				var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();

				if (role == "doctor")
				{
					var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.UserId == user.Id);
					if (doctor is null) return NotFound("Doctor does not exist");
					id = doctor.Id;
				}
				else if (role == "staff")
				{
					var staff = await context.StaffMembers.FirstOrDefaultAsync(d => d.UserId == user.Id);
					if (staff is null) return NotFound("Staff member does not exist.");
					id = staff.Id;
				}
				else if (role == "patient")
				{
					var patient = await context.Patients.FirstOrDefaultAsync(d => d.UserId == user.Id);
					if (patient is null) return NotFound("Patient does not exist.");
					id = patient.Id;
				}

				return Ok(new
				{
					Token = token,
					Role = role,
					Id = id
				});
            }
            catch (Exception ex)
			{
                return StatusCode(500, new { message = "Login failed", errors = ex.Message });
            }
        }

		[HttpDelete]
		public async Task<ActionResult> DeleteAllUsers()
		{
			try
			{
				var users = userManager.Users.ToList();

				foreach (var user in users)
				{
					var result = await userManager.DeleteAsync(user);
					if (!result.Succeeded)
					{
						return BadRequest(new
						{
							message = $"Failed to delete user: {user.Email}",
							errors = result.Errors
						});
					}
				}

				return Ok(new { message = $"{users.Count} users deleted successfully." });
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return StatusCode(500, new { message = "Internal server error", error = ex.Message });
			}
		}
	}
}
