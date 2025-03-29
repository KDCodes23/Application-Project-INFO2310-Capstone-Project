using HealthHorizon_API.Data;
using HealthHorizon_API.Interfaces;
using HealthHorizon_API.Models.Entities;
using HealthHorizon_API.Models.Identities;
using HealthHorizon_API.Services;
using Microsoft.AspNetCore.Authorization;
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

			try
			{
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingUser = await userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email already registered!");
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
                    HospitalName = request.Doctor.HospitalName,
                    ProfessionalBio = request.Doctor.ProfessionalBio,
                    UserId = user.Id,
                    User = user
                };

                await context.Doctors.AddAsync(doctor);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while saving patient data");
            }
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

			try
			{
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingUser = await userManager.FindByEmailAsync(request.Email);
				if (existingUser != null)
				{
					return BadRequest("Email already registered!");
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
                    return BadRequest(new { message = "User creation failed", errors = result.Errors });
                }

                await userManager.AddToRoleAsync(user, "patient");

				var patient = new Patient
				{
					FirstName = request.Patient.FirstName,
                    LastName = request.Patient.LastName,
                    DateOfBirth = request.Patient.DateOfBirth,
					PhoneNumber = request.Patient.PhoneNumber,
                    Gender = request.Patient.Gender,
                    AddressId = request.Patient.AddressId,
					UserId = user.Id,
					User = user
				};

                if (request.Patient.Address != null)
                {
                    var address = new Address
                    {
                        Street = request.Patient.Address.Street,
                        City = request.Patient.Address.City,
                        ProvinceOrState = request.Patient.Address.ProvinceOrState,
                        Country = request.Patient.Address.Country,
                        PostalCode = request.Patient.Address.PostalCode
                    };

                    context.Addresses.Add(address);
                    await context.SaveChangesAsync();
                    patient.AddressId = address.Id;
                }

                await context.Patients.AddAsync(patient);
				await context.SaveChangesAsync();

                return Ok(new { message = "Patient registered successfully" });

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
			try
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
                var token = jwtTokenService.GenerateJwtTokenAsync(user);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
			{
                return StatusCode(500, new { message = "Login failed", errors = ex.Message });
            }
        }
	}
}
