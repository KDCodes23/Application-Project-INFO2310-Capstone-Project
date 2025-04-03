﻿using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using HealthHorizon_API.Models.UtilityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeedbackController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public FeedbackController(HealthHorizonContext context)
		{
			this.context = context;
		}

		//[Authorize(Roles = "admin")]
		[HttpGet]
		public async Task<ActionResult<List<Feedback>>> GetAllFeedbacks()
		{
			var feedbacks = await context.Feedbacks.ToListAsync();
			if (feedbacks is null)
			{
				return NotFound("Feedbacks Not Found");
			}

			return Ok(feedbacks);
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpGet("get-feedback")]
		public async Task<ActionResult<Feedback>> GetFeedback([FromQuery] Guid id)
		{
			if (id == Guid.Empty)
			{
				return BadRequest("Id Required");
			}

			var feedback = await context.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
			if (feedback is null)
			{
				return NotFound("Feedback Not Found");
			}

			return Ok(feedback);
		}

		//[Authorize(Roles = "admin, patient")]
		[HttpPost]
		public async Task<ActionResult> PostFeedback([FromBody] Feedback newFeedback)
		{
			if (newFeedback is null)
			{
				return BadRequest("Feedback Data Required");
			}

			await context.Feedbacks.AddAsync(newFeedback);
			await context.SaveChangesAsync();

			return Created();
		}

		//[Authorize(Roles = "admin, patient")]
		[HttpPut]
		public async Task<ActionResult> UpdateFeddback([FromBody] Feedback newFeedback)
		{
			if (newFeedback is null)
			{
				return BadRequest("Feedback Data Required");
			}

			var feedbackDB = await context.Feedbacks.FirstOrDefaultAsync(f => f.Id == newFeedback.Id);
			if (feedbackDB is null)
			{
				return NotFound("Feedback Not Found");
			}

			feedbackDB.Details = newFeedback.Details;
			await context.SaveChangesAsync();

			return Ok("Feedback Updated");
		}

		//[Authorize(Roles = "admin")]
		[HttpDelete("delete-feedback")]
		public async Task<ActionResult> DeleteFeedback([FromQuery] Guid id)
		{
			if (id == Guid.Empty)
			{
				return BadRequest("Id Required");
			}

			var feedback = await context.Feedbacks.FirstOrDefaultAsync(fb => fb.Id == id);
			if (feedback is null)
			{
				return NotFound("Feedback Not Found");
			}

			context.Feedbacks.Remove(feedback);
			await context.SaveChangesAsync();

			return Ok("Feedback Deleted");
		}
	}
}
