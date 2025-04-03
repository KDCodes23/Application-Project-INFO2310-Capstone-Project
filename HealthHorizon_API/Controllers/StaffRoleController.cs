using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StaffRoleController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public StaffRoleController(HealthHorizonContext context) => this.context = context;

		//[Authorize(Roles = "admin, doctor, staff")]
		[HttpGet]
		public async Task<ActionResult<List<StaffRole>>> GetAllRoles()
		{
			var roles = await context.Roles.ToListAsync();
			if (roles is null) return NotFound("Role Not Found");

			return Ok(roles);
		}

		//[Authorize(Roles = "admin, doctor, staff")]
		[HttpGet("get-role")]
		public async Task<ActionResult<StaffRole>> GetRole([FromQuery] Guid id)
		{
			if (id == Guid.Empty) return BadRequest("Id Required");

			var role = await context.StaffRoles.FindAsync(id);
			if (role is null) return NotFound("Role Not Found");

			return Ok(role);
		}

		//[Authorize(Roles = "admin")]
		[HttpPost]
		public async Task<ActionResult> PostRole([FromBody] StaffRole newRole)
		{
			if (newRole is null) return BadRequest("Role Data Required");

			await context.StaffRoles.AddAsync(newRole);
			await context.SaveChangesAsync();

			return Created();
		}

		//[Authorize(Roles = "admin")]
		[HttpPut]
		public async Task<ActionResult> UpdateRole([FromBody] StaffRole newRole)
		{
			if (newRole is null) return BadRequest("Role Data Required");

			var role = await context.StaffRoles.FindAsync(newRole.Id);
			if (role is null) return NotFound("Role Not Found");

			role.Title = newRole.Title;
			role.Description = newRole.Description;
			await context.SaveChangesAsync();

			return Ok("Staff Role Updated");
		}

		//[Authorize(Roles = "admin")]
		[HttpDelete("delete-role")]
		public async Task<ActionResult> DeleteRole([FromQuery] Guid id)
		{
			if (id == Guid.Empty) return BadRequest("Id Required");

			var role = await context.StaffRoles.FirstOrDefaultAsync(x => x.Id == id);
			if (role is null) return NotFound("Role Not Found");

			context.StaffRoles.Remove(role);
			await context.SaveChangesAsync();

			return Ok("Staff Role Deleted");
		}
	}
}
