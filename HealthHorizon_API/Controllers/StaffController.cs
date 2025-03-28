using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StaffController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public StaffController(HealthHorizonContext context)
		{
			this.context = context;
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		[HttpGet]
		public async Task<ActionResult<List<Staff>>> GetAllStaff()
		{
			var staff = await context.StaffMembers.Include(s => s.Role).ToListAsync();
			if (staff == null)
			{
				return NotFound();
			}

			return Ok(staff);
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "doctor")]
		//[Authorize(Roles = "staff")]
		[HttpGet("{id:int}")]
		public async Task<ActionResult<Staff>> GetStaff([FromQuery] int id)
		{
			var staff = await context.StaffMembers.Include(s => s.Role).FirstOrDefaultAsync(s => s.Id == id);
			if (staff == null)
			{
				return NotFound();
			}

			return Ok(staff);
		}

		//[Authorize(Roles = "admin")]
		[HttpPost("add-staff")]
		public async Task<ActionResult> PostStaff([FromBody] Staff staff)
		{
			if (staff == null)
			{
				return BadRequest();
			}

			await context.StaffMembers.AddAsync(staff);
			await context.SaveChangesAsync();

			return Ok();
		}

		//[Authorize(Roles = "admin")]
		[HttpPut("update-staff")]
		public async Task<ActionResult> UpdateStaff([FromBody] Staff staff)
		{
			var staffDB = await context.StaffMembers.FirstOrDefaultAsync(s => s.Id == staff.Id);
			if (staffDB == null)
			{
				return BadRequest();
			}

			staffDB.Name = staff.Name;
			staffDB.User.Email = staff.User.Email;
			staffDB.PhoneNumber = staff.PhoneNumber;
			staffDB.RoleId = staff.RoleId;
			await context.SaveChangesAsync();

			return Ok();
		}

		//[Authorize(Roles = "admin")]
		[HttpDelete("remove-{id:int}")]
		public async Task<ActionResult> DeleteStaff([FromQuery] int id)
		{
			var staff = await context.StaffMembers.FirstOrDefaultAsync(s => s.Id == id);
			if (staff == null)
			{
				return NotFound();
			}

			context.StaffMembers.Remove(staff);
			await context.SaveChangesAsync();

			return Ok();
		}
	}
}
