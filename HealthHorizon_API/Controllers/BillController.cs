﻿using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
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

		[Authorize(Roles = "admin")]
		[HttpGet]
		public async Task<ActionResult<List<Bill>>> GetAllBills()
		{
			var bills = await context.Bills.Include(b => b.Appointment).ToListAsync();
			if (bills == null)
			{
				return NotFound();
			}

			return Ok(bills);
		}

		[Authorize(Roles = "admin")]
		[Authorize(Roles = "doctor")]
		[Authorize(Roles = "staff")]
		[Authorize(Roles = "patient")]
		[HttpGet("{id}")]
		public async Task<ActionResult<Bill>> GetBill(int id)
		{
			var bill = await context.Bills.Include(b => b.Appointment).FirstOrDefaultAsync(b => b.Id == id);
			if (bill == null)
			{
				return NotFound();
			}

			return Ok(bill);
		}

		[Authorize(Roles = "admin")]
		[Authorize(Roles = "doctor")]
		[HttpPost]
		public async Task<ActionResult> PostBill([FromBody] Bill bill)
		{
			if (bill == null)
			{
				return BadRequest();
			}

			await context.Bills.AddAsync(bill);
			await context.SaveChangesAsync();

			return Ok();
		}

		[Authorize(Roles = "admin")]
		[Authorize(Roles = "doctor")]
		[HttpPut]
		public async Task<ActionResult> UpdateBill([FromBody] Bill bill)
		{
			var billDB = await context.Bills.FirstOrDefaultAsync(b => b.Id == bill.Id);
			if (billDB == null)
			{
				return BadRequest();
			}
			
			billDB.Amount = bill.Amount;
			billDB.PaymentMethod = bill.PaymentMethod;
			billDB.Date = bill.Date;
			billDB.AppointmentId = bill.AppointmentId;
			await context.SaveChangesAsync();

			return Ok();
		}

		[Authorize(Roles = "admin")]
		[HttpDelete]
		public async Task<ActionResult> DeleteBill(int id)
		{
			var bill = await context.Bills.FirstOrDefaultAsync(b => b.Id == id);
			if (bill == null)
			{
				return NotFound();
			}

			context.Bills.Remove(bill);
			await context.SaveChangesAsync();

			return Ok();
		}
	}
}
