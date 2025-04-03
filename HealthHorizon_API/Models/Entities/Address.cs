using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.Entities
{
	public class Address
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		public string Street { get; set; } = string.Empty;
		[Required]
		public string City { get; set; } = string.Empty;
		[Required]
		public string ProvinceOrState { get; set; } = string.Empty;
		[Required]
		public string Country { get; set; } = string.Empty;
		[Required]
		public string PostalCode { get; set; } = string.Empty;
	}
}
