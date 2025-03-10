using HealthHorizon_API.Data;
using HealthHorizon_API.Models.PersonTypes;
using Microsoft.AspNetCore.Http;
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

		[HttpGet]
		public async Task<ActionResult<List<Staff>>> GetAllStaff()
		{
			var staff = await context.Staffs.ToListAsync();
			if (staff == null)
			{
				return NotFound();
			}

			return Ok(staff);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Staff>> GetStaff(int id)
		{
			var staff = await context.Staffs.FirstOrDefaultAsync(s => s.Id == id);
			if (staff == null)
			{
				return NotFound();
			}

			return Ok(staff);
		}

		[HttpPost]
		public async Task<ActionResult> PostStaff(Staff staff)
		{
			if (staff == null)
			{
				return BadRequest();
			}

			await context.Staffs.AddAsync(staff);
			await context.SaveChangesAsync();

			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> UpdateStaff(Staff staff)
		{
			var staffDB = await context.Staffs.FirstOrDefaultAsync(s => s.Id == staff.Id);
			if (staffDB == null)
			{
				return BadRequest();
			}

			staffDB.Name = staff.Name;
			staffDB.Email = staff.Email;
			staffDB.PhoneNumber = staff.PhoneNumber;
			staffDB.RoleId = staff.RoleId;
			await context.SaveChangesAsync();

			return Ok();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteStaff(int id)
		{
			var staff = await context.Staffs.FirstOrDefaultAsync(s => s.Id == id);
			if (staff == null)
			{
				return NotFound();
			}

			context.Staffs.Remove(staff);
			await context.SaveChangesAsync();

			return Ok();
		}
	}
}
