using Microsoft.AspNetCore.Mvc;
using HealthHorizon_API.Data;
using HealthHorizon_API.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly HealthHorizonContext _context;

        public LoginController(HealthHorizonContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginModel model)
        {
            if (model.Role == "patient")
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Email == model.Email);

                if (patient != null && patient.Password == model.Password)
                    return Ok(new { success = true, role = "patient" });
            }
            else if (model.Role == "doctor")
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Email == model.Email);

                if (doctor != null && doctor.Password == model.Password)
                    return Ok(new { success = true, role = "doctor" });
            }

            return Unauthorized("Invalid email, password, or role!");
        }
    }

    public class LoginModel
    {
        [Required]
        public string Role { get; set; } // "patient" or "doctor"

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}