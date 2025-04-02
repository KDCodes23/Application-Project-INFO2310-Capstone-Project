using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.DTOs
{
	public class DoctorDTO
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Specialization { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string HospitalName { get; set; } = string.Empty;
		public string ProfessionalBio { get; set; } = string.Empty;
	}
}
