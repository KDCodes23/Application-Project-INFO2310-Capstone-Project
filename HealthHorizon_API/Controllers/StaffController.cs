using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using HealthHorizon_API.Models.UtilityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StaffController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public StaffController(HealthHorizonContext context) => this.context = context;

		//[Authorize(Roles = "admin, doctor")]
		[HttpGet]
		public async Task<ActionResult<List<Staff>>> GetAllStaff()
		{
			var staff = await context.StaffMembers.Include(s => s.Role).ToListAsync();
			if (staff is null) return NotFound("Staff Not Found");

			return Ok(staff);
		}

		//[Authorize(Roles = "admin, doctor, staff")]
		[HttpGet("get-staff-member")]
		public async Task<ActionResult<Staff>> GetStaff([FromBody] IdRequest request)
		{
			if (request is null || request.Id == Guid.Empty) return BadRequest("Id Required");

			var staff = await context.StaffMembers.Include(s => s.Role).FirstOrDefaultAsync(s => s.Id == request.Id);
			if (staff is null) return NotFound("Staff Not Found");

			return Ok(staff);
		}

		//[Authorize(Roles = "admin")]
		[HttpPost]
		public async Task<ActionResult> PostStaff([FromBody] Staff staff)
		{
			if (staff is null) return BadRequest("Staff Data Required");

			await context.StaffMembers.AddAsync(staff);
			await context.SaveChangesAsync();

			return Created();
		}

		//[Authorize(Roles = "admin")]
		[HttpPut]
		public async Task<ActionResult> UpdateStaff([FromBody] Staff newStaff)
		{
			if (newStaff is null) return BadRequest("Staff Data Required");

			var staffDB = await context.StaffMembers.FirstOrDefaultAsync(s => s.Id == newStaff.Id);
			if (staffDB is null) return BadRequest("Data Required");

			staffDB.Name = newStaff.Name;
			staffDB.User.Email = newStaff.User.Email;
			staffDB.PhoneNumber = newStaff.PhoneNumber;
			staffDB.RoleId = newStaff.RoleId;
			await context.SaveChangesAsync();

			return Ok();
		}

		//[Authorize(Roles = "admin")]
		[HttpDelete("delete-staff-member")]
		public async Task<ActionResult> DeleteStaff([FromBody] IdRequest request)
		{
			var staff = await context.StaffMembers.FirstOrDefaultAsync(s => s.Id == request.Id);
			if (staff is null) return NotFound("Staff Not Found");

			context.StaffMembers.Remove(staff);
			await context.SaveChangesAsync();

			return Ok();
		}
	}
}
