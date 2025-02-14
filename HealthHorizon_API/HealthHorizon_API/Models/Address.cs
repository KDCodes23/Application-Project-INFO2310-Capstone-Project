using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthHorizon_API.Models
{
	public class Address
	{
		public string Street { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string ProvinceOrState { get; set; } = string.Empty;
		public string Country {  get; set; } = string.Empty;
		public string PostalCode {  get; set; } = string.Empty;
	}
}
