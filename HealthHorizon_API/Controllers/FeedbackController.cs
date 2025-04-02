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
			if (feedbacks == null)
			{
				return NotFound();
			}

			return Ok(feedbacks);
		}

		//[Authorize(Roles = "admin, doctor")]
		[HttpGet("get-feedback")]
		public async Task<ActionResult<Feedback>> GetFeedback([FromBody] IdRequest request)
		{
			var feedback = await context.Feedbacks.FirstOrDefaultAsync(f => f.Id == request.Id);
			if (feedback == null)
			{
				return NotFound();
			}

			return Ok(feedback);
		}

		//[Authorize(Roles = "admin, patient")]
		[HttpPost]
		public async Task<ActionResult> PostFeedback([FromBody] Feedback feedback)
		{
			if (feedback == null)
			{
				return BadRequest();
			}

			await context.Feedbacks.AddAsync(feedback);
			await context.SaveChangesAsync();

			return Ok();
		}

		//[Authorize(Roles = "admin, patient")]
		[HttpPut]
		public async Task<ActionResult> UpdateFeddback([FromBody] Feedback feedback)
		{
			var feedbackDB = await context.Feedbacks.FirstOrDefaultAsync(f => f.Id == feedback.Id);
			if (feedbackDB == null)
			{
				return NotFound();
			}

			feedbackDB.Details = feedback.Details;
			await context.SaveChangesAsync();

			return Ok();
		}

		//[Authorize(Roles = "admin")]
		[HttpDelete("delete-feedback")]
		public async Task<ActionResult> DeleteFeedback([FromBody] IdRequest request)
		{
			var feedback = await context.Feedbacks.FirstOrDefaultAsync(fb => fb.Id == request.Id);
			if (feedback == null)
			{
				return NotFound();
			}

			context.Feedbacks.Remove(feedback);
			await context.SaveChangesAsync();

			return Ok();
		}
	}
}
