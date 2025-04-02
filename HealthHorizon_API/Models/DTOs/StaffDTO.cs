using HealthHorizon_API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthHorizon_API.Models.DTOs
{
	public class StaffDTO
	{
		public string Name { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public Guid RoleId { get; set; }
	}
}
