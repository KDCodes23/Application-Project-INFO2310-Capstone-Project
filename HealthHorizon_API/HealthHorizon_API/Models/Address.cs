using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HealthHorizon_API.Models.PersonTypes;

namespace HealthHorizon_API.Models
{
	public class Address
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Street { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string ProvinceOrState { get; set; } = string.Empty;
		public string Country {  get; set; } = string.Empty;
		public string PostalCode {  get; set; } = string.Empty;

        public Patient? Patient { get; set; } = null!;
    }
}
