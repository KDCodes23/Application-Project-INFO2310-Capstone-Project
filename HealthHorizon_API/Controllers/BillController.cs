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
			if (bills == null)
			{
				return NotFound("Bills Not Found");
			}

			return Ok(bills);
		}

		//[Authorize]
		[HttpGet("get-bill")]
		public async Task<ActionResult<Bill>> GetBill([FromBody] IdRequest request)
		{
			var bill = await context.Bills.Include(b => b.Appointment).FirstOrDefaultAsync(b => b.Id == request.Id);
			if (bill == null)
			{
				return NotFound("Bill Not Found");
			}

			return Ok(bill);
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpPost]
		public async Task<ActionResult> PostBill([FromBody] Bill bill)
		{
			if (bill == null)
			{
				return BadRequest("Bill Data Required");
			}

			await context.Bills.AddAsync(bill);
			await context.SaveChangesAsync();

			return Created();
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpPut]
		public async Task<ActionResult> UpdateBill([FromBody] Bill bill)
		{
			var billDB = await context.Bills.FirstOrDefaultAsync(b => b.Id == bill.Id);
			if (billDB == null)
			{
				return BadRequest("Bill Data Required");
			}
			
			billDB.Amount = bill.Amount;
			billDB.PaymentMethod = bill.PaymentMethod;
			billDB.Date = bill.Date;
			billDB.AppointmentId = bill.AppointmentId;
			await context.SaveChangesAsync();

			return Ok("Bill Updated");
		}

		//[Authorize(Roles = "admin")]
		[HttpDelete("delete-bill")]
		public async Task<ActionResult> DeleteBill([FromBody] IdRequest request)
		{
			var bill = await context.Bills.FirstOrDefaultAsync(b => b.Id == request.Id);
			if (bill == null)
			{
				return NotFound("Bill Not Found");
			}

			context.Bills.Remove(bill);
			await context.SaveChangesAsync();

			return Ok("Bill Deleted");
		}
	}
}
