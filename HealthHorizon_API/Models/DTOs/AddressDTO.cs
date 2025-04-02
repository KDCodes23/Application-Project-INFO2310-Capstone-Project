using HealthHorizon_API.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.DTOs
{
	public class AddressDTO
	{
		public string Street { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string ProvinceOrState { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;
	}
}
