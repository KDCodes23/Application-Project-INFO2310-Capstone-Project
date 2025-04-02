using HealthHorizon_API.Data;
using HealthHorizon_API.Models.Entities;
using HealthHorizon_API.Models.UtilityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

		//[Authorize(Roles = "admin")]
		[HttpGet]
		public async Task<ActionResult<List<AIChatLog>>> GetAllAiChatLogs()
		{
			var logs = await context.AIChatLogs.Include(l => l.Patient).ToListAsync();
			if (logs is null)
			{
				return NotFound("Chat Logs Not Found");
			}

			return Ok(logs);
		}

		[HttpGet("patient-chat-logs")]
		public async Task<ActionResult<List<AIChatLog>>> GetPatientChatLogs([FromBody] IdRequest request)
		{
			if (request is null || request.Id == Guid.Empty)
			{
				return BadRequest("Id Requred");
			}

			var logs = await context.AIChatLogs.Include(l => l.Patient).Where(l => l.PatientId == request.Id).ToListAsync();
			if (logs is null)
			{
				return NotFound("Chat Logs Not Found");
			}

			return Ok(logs);	
		}

		//[Authorize(Roles = "admin, patient")]
		[HttpGet("get-chat-log")]
		public async Task<ActionResult<AIChatLog>> GetAiChatLog([FromBody] IdRequest request)
		{
			if (request is null || request.Id == Guid.Empty)
			{
				return BadRequest("Id Requred");
			}

			var log = await context.AIChatLogs.Include(l => l.Patient).FirstOrDefaultAsync(l => l.Id == request.Id);
			if (log is null)
			{
				return NotFound("Chat Log Not Found");
			}

			return Ok(log);
		}

		//[Authorize(Roles = "admin, patient")]
		[HttpPost]
		public async Task<ActionResult> PostAiChatLog([FromBody] AIChatLog log)
		{
			if (log is null)
			{
				return BadRequest("Chat Log Data Required");
			}

			await context.AddAsync(log);
			await context.SaveChangesAsync();
			return Created();
		}

		//[Authorize(Roles = "admin, patient")]
		[HttpPut]
		public async Task<ActionResult> UpdateAiChatLog([FromBody] AIChatLog log)
		{
			if (log is null)
			{
				return BadRequest("Chat Log Data Required");
			}

			var logDB = await context.AIChatLogs.FirstOrDefaultAsync(l => l.Id == log.Id);
			if (logDB is null)
			{
				return NotFound("Chat Log Not Found");
			}

			logDB.Date = log.Date;
			logDB.Content = log.Content;
			logDB.PatientId = log.PatientId;

			await context.SaveChangesAsync();
			return Ok("Chat Log Updated");
		}

		//[Authorize(Roles = "admin")]
		[HttpDelete("{id:int}")]
		public async Task<ActionResult> DeleteAiChatLog([FromBody] IdRequest request)
		{
			if (request is null || request.Id == Guid.Empty)
			{
				return BadRequest("Id Requred");
			}

			var log = await context.AIChatLogs.FirstOrDefaultAsync(l => l.Id == request.Id);
			if (log is null)
			{
				return NotFound("Chat Log Data Required");
			}

			context.AIChatLogs.Remove(log);
			await context.SaveChangesAsync();

			return Ok("Chat Log Deleted");
		}
	}
}
