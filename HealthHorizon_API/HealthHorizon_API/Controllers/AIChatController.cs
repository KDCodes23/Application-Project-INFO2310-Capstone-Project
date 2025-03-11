using HealthHorizon_API.Data;
using HealthHorizon_API.Models.PersonTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AIChatController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public AIChatController(HealthHorizonContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<AIChatLog>>> GetAllAiChatLogs()
		{
			var logs = await context.AIChatLogs.Include(l => l.Patient).ToListAsync();
			if (logs == null)
			{
				return NotFound();
			}

			return Ok(logs);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<AIChatLog>> GetAiChatLog(int id)
		{
			var log = await context.AIChatLogs.Include(l => l.Patient).FirstOrDefaultAsync(l => l.Id == id);
			if (log == null)
			{
				return NotFound();
			}

			return Ok(log);
		}

		[HttpPost]
		public async Task<ActionResult> PostAiChatLog(AIChatLog log)
		{
			if (log == null)
			{
				return BadRequest();
			}

			await context.AddAsync(log);
			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> UpdateAiChatLog(AIChatLog log)
		{
			var logDB = await context.AIChatLogs.FirstOrDefaultAsync(l => l.Id == log.Id);
			if (logDB == null)
			{
				return NotFound();
			}

			logDB.Date = log.Date;
			logDB.Content = log.Content;
			logDB.PatientId = log.PatientId;

			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteAiChatLog(int id)
		{
			var log = await context.AIChatLogs.FirstOrDefaultAsync(l => l.Id == id);
			if (log == null)
			{
				return NotFound();
			}

			context.AIChatLogs.Remove(log);
			await context.SaveChangesAsync();
			return Ok();
		}
	}
}
