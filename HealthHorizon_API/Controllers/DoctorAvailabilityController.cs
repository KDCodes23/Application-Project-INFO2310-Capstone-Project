using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthHorizon_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAvailabilityController : ControllerBase
    {
        private readonly HealthHorizonContext context;

        public DoctorAvailabilityController(HealthHorizonContext context)
        {
            this.context = context;
        }

        // Get all availability for a specific doctor
        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetDoctorAvailability(int doctorId)
        {
            var availability = await context.DoctorAvailabilities
                                             .Where(da => da.DoctorId == doctorId)
                                             .ToListAsync();

            if (availability == null || !availability.Any())
                return NotFound();

            return Ok(availability);
        }

        // Add or update availability for a doctor
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateAvailability([FromBody] DoctorAvailability availability)
        {
            if (availability == null)
            {
                return BadRequest("Invalid data.");
            }

            if (availability.DoctorId == 0)
            {
                return BadRequest("DoctorId is required.");
            }

            if (availability.Date == null)
            {
                return BadRequest("Date is required.");
            }

            // Skip DoctorId match and allow anyone to schedule any availability
            // You could re-enable this validation if needed: 
            // var currentDoctorId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "DoctorId")?.Value);
            // if (availability.DoctorId != currentDoctorId)
            // {
            //     return Unauthorized("You can only manage your own availability.");
            // }

            // Validate DoctorId exists in the Doctors table
            var doctorExists = await context.Doctors.AnyAsync(d => d.Id == availability.DoctorId);
            if (!doctorExists)
                return NotFound("Doctor not found.");

            // Check if availability already exists for that doctor and date
            var existingAvailability = await context.DoctorAvailabilities
                                                      .FirstOrDefaultAsync(da => da.DoctorId == availability.DoctorId && da.Date == availability.Date);

            if (existingAvailability != null)
            {
                // Update existing availability
                existingAvailability.ShiftStart = availability.ShiftStart;
                existingAvailability.ShiftEnd = availability.ShiftEnd;
                existingAvailability.IsAvailable = availability.IsAvailable;
                context.Update(existingAvailability);
            }
            else
            {
                // Add new availability
                context.DoctorAvailabilities.Add(availability);
            }

            await context.SaveChangesAsync();
            return Ok(availability);
        }
    }
}
