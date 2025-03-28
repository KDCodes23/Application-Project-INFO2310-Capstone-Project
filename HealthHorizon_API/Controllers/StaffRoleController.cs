using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StaffRoleController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public StaffRoleController(HealthHorizonContext context)
		{
			this.context = context;
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "staff")]
		[HttpGet]
		public async Task<ActionResult<List<StaffRole>>> GetAllRoles()
		{
			var roles = await context.Roles.ToListAsync();
			if (roles == null)
			{
				return NotFound();
			}
			return Ok(roles);
		}

		//[Authorize(Roles = "admin")]
		//[Authorize(Roles = "staff")]
		[HttpGet("{id:int}")]
		public async Task<ActionResult<StaffRole>> GetRole([FromQuery] int id)
		{
			var role = await context.StaffRoles.FirstOrDefaultAsync(x => x.Id == id);
			if (role == null)
			{
				return NotFound();
			}
			return Ok(role);
		}

		//[Authorize(Roles = "admin")]
		[HttpPost]
		public async Task<ActionResult> PostRole([FromBody] StaffRole newRole)
		{
			await context.StaffRoles.AddAsync(newRole);
			await context.SaveChangesAsync();
			return Ok();
		}

		//[Authorize(Roles = "admin")]
		[HttpPut]
		public async Task<ActionResult> UpdateRole([FromBody] StaffRole newRole)
		{
			var role = await context.StaffRoles.FirstOrDefaultAsync(x => x.Id == newRole.Id);
			if (role == null)
			{
				return NotFound();
			}
			role.Title = newRole.Title;
			role.Description = newRole.Description;
			await context.SaveChangesAsync();
			return Ok();
		}

		//[Authorize(Roles = "admin")]
		[HttpDelete("{id:int}")]
		public async Task<ActionResult> DeleteRole([FromQuery] int id)
		{
			var role = await context.StaffRoles.FirstOrDefaultAsync(x => x.Id == id);
			if (role == null)
			{
				return NotFound();
			}
			context.StaffRoles.Remove(role);
			await context.SaveChangesAsync();
			return Ok();
		}
	}
}
