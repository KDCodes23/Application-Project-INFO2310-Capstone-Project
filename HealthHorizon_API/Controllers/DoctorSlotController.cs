using HealthHorizon_API.Models.Entities;
using HealthHorizon_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSlotController : ControllerBase
    {
        private readonly HealthHorizonContext context;

        public DoctorSlotController(HealthHorizonContext context)
        {
            this.context = context;
        }

        [HttpPost("GenerateSlots")]
        public async Task<ActionResult> GenerateDoctorSlots()
        {
            var doctorAvailabilities = await context.DoctorAvailabilities
                //.Include(da => da.Doctor)
                .ToListAsync();

            List<DoctorSlot> newSlots = new List<DoctorSlot>();

            foreach (var availability in doctorAvailabilities)
            {
                // Generate time slots for each availability
                TimeSpan shiftStart = availability.ShiftStart;
                TimeSpan shiftEnd = availability.ShiftEnd;

                // Loop to generate 1-hour slots
                while (shiftStart < shiftEnd)
                {
                    TimeSpan slotEnd = shiftStart.Add(new TimeSpan(1, 0, 0)); // Add one hour for slot end time
                    if (slotEnd > shiftEnd) break;  // Ensure slot doesn't exceed shift end

                    var newSlot = new DoctorSlot
                    {
                        DoctorId = availability.DoctorId,
                        SlotDate = availability.Date,
                        SlotStart = shiftStart,
                        SlotEnd = slotEnd
                    };

                    newSlots.Add(newSlot);
                    shiftStart = slotEnd;  // Move to the next slot
                }
            }

            await context.DoctorSlots.AddRangeAsync(newSlots);
            await context.SaveChangesAsync();

            return Ok(new { Message = "Slots generated successfully!", Count = newSlots.Count });
        }


        [HttpGet("Doctor/{doctorId}")]
        public async Task<ActionResult<List<DoctorSlot>>> GetSlotsForDoctor(int doctorId)
        {
            var doctorSlots = await context.DoctorSlots
                .Where(ds => ds.DoctorId == doctorId)
                .ToListAsync();

            if (doctorSlots == null || !doctorSlots.Any())
            {
                return NotFound(new { Message = "No slots available for this doctor." });
            }

            return Ok(doctorSlots);
        }


        [HttpGet("Doctor/{doctorId}/Date/{date}")]
        public async Task<ActionResult<List<DoctorSlot>>> GetSlotsForDoctorOnDate(int doctorId, DateTime date)
        {
            var doctorSlots = await context.DoctorSlots
                .Where(ds => ds.DoctorId == doctorId && ds.SlotDate.Date == date.Date)
                .ToListAsync();

            if (doctorSlots == null || !doctorSlots.Any())
            {
                return NotFound(new { Message = "No slots available for this doctor on the specified date." });
            }

            return Ok(doctorSlots);
        }

        // PUT: api/DoctorSlot/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctorSlot(int id, DoctorSlot doctorSlot)
        {
            if (id != doctorSlot.DoctorSlotId)
            {
                return BadRequest("Slot ID mismatch.");
            }

            context.Entry(doctorSlot).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/DoctorSlot/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctorSlot(int id)
        {
            var doctorSlot = await context.DoctorSlots.FindAsync(id);

            if (doctorSlot == null)
            {
                return NotFound();
            }

            context.DoctorSlots.Remove(doctorSlot);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorSlot>> GetSlotById(int id)
        {
            var slot = await context.DoctorSlots
                .FirstOrDefaultAsync(ds => ds.DoctorId == id);

            if (slot == null)
            {
                return NotFound(new { Message = "Slot not found." });
            }

            return Ok(slot);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSlot(int id, [FromBody] DoctorSlot updatedSlot)
        {
            var existingSlot = await context.DoctorSlots
                .FirstOrDefaultAsync(ds => ds.DoctorId == id);

            if (existingSlot == null)
            {
                return NotFound(new { Message = "Slot not found." });
            }

            existingSlot.SlotStart = updatedSlot.SlotStart;
            existingSlot.SlotEnd = updatedSlot.SlotEnd;
            existingSlot.SlotDate = updatedSlot.SlotDate;

            await context.SaveChangesAsync();

            return Ok(new { Message = "Slot updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSlot(int id)
        {
            var slot = await context.DoctorSlots
                .FirstOrDefaultAsync(ds => ds.DoctorId == id);

            if (slot == null)
            {
                return NotFound(new { Message = "Slot not found." });
            }

            context.DoctorSlots.Remove(slot);
            await context.SaveChangesAsync();

            return Ok(new { Message = "Slot deleted successfully." });
        }

        [HttpGet]
        public async Task<ActionResult<List<DoctorSlot>>> GetAllSlots()
        {
            var allSlots = await context.DoctorSlots.ToListAsync();

            if (allSlots == null || !allSlots.Any())
            {
                return NotFound(new { Message = "No slots available." });
            }

            return Ok(allSlots);
        }
    }
}
