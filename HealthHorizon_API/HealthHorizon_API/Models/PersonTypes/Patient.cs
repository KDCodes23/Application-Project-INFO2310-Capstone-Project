using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHorizon_API.Models.PersonTypes
{
	public class Patient
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
        public string? FirstName { get; set; }  // Nullable
        public string? LastName { get; set; }   // Nullable
        public string? Email { get; set; }      // Nullable
        public string? PhoneNumber { get; set; }      // Nullable
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? MedicalConditions { get; set; }
        [Required]
        public string? Password { get; set; }

        public int AddressId { get; set; }
        public Address? Address { get; set; } = null!;
    }
}
