using HealthHorizon_API.Data;
using HealthHorizon_API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthHorizon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MedicalRecordController : ControllerBase
	{
		private readonly HealthHorizonContext context;

		public MedicalRecordController(HealthHorizonContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<MedicalRecordSmallDTO>>> GetAllMedicalRecords()
		{
			return Ok();
		}
	}
}
