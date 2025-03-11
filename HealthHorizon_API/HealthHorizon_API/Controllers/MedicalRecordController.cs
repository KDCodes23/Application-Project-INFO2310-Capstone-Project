using HealthHorizon_API.Data;
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
	}
}
