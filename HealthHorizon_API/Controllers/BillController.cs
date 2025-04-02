using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using HealthHorizon_API.Models.UtilityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BillController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public BillController(HealthHorizonContext context)
		{
			this.context = context;
		}

		//[Authorize(Roles = "admin")]
		[HttpGet]
		public async Task<ActionResult<List<Bill>>> GetAllBills()
		{
			var bills = await context.Bills.Include(b => b.Appointment).ToListAsync();
			if (bills is null)
			{
				return NotFound("Bills Not Found");
			}

			return Ok(bills);
		}

		//[Authorize]
		[HttpGet("get-bill")]
		public async Task<ActionResult<Bill>> GetBill([FromBody] IdRequest request)
		{
			if (request is null || request.Id == Guid.Empty)
			{
				return BadRequest("Id Required");
			}

			var bill = await context.Bills.Include(b => b.Appointment).FirstOrDefaultAsync(b => b.Id == request.Id);
			if (bill is null)
			{
				return NotFound("Bill Not Found");
			}

			return Ok(bill);
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpPost]
		public async Task<ActionResult> PostBill([FromBody] Bill newBill)
		{
			if (newBill is null)
			{
				return BadRequest("Bill Data Required");
			}

			await context.Bills.AddAsync(newBill);
			await context.SaveChangesAsync();

			return Created();
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpPut]
		public async Task<ActionResult> UpdateBill([FromBody] Bill newBill)
		{
			if (newBill is null)
			{
				return BadRequest("Bill Data Required");
			}

			var billDB = await context.Bills.FirstOrDefaultAsync(b => b.Id == newBill.Id);
			if (billDB is null)
			{
				return NotFound("Bill NotFound");
			}
			
			billDB.Amount = newBill.Amount;
			billDB.PaymentMethod = newBill.PaymentMethod;
			billDB.Date = newBill.Date;
			billDB.AppointmentId = newBill.AppointmentId;
			await context.SaveChangesAsync();

			return Ok("Bill Updated");
		}

		//[Authorize(Roles = "admin")]
		[HttpDelete("delete-bill")]
		public async Task<ActionResult> DeleteBill([FromBody] IdRequest request)
		{
			if (request is null || request.Id == Guid.Empty)
			{
				return BadRequest("Id Required");
			}

			var bill = await context.Bills.FirstOrDefaultAsync(b => b.Id == request.Id);
			if (bill is null)
			{
				return NotFound("Bill Not Found");
			}

			context.Bills.Remove(bill);
			await context.SaveChangesAsync();

			return Ok("Bill Deleted");
		}
	}
}
