﻿using HealthHorizon_API.Models.PersonTypes;

namespace HealthHorizon_API.Models.Medical_Record_Types
{
	public class AllergyTest
	{
		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public bool Peanut { get; set; } = false;
		public bool TreeNut { get; set; } = false;
		public bool Dairy { get; set; } = false;
		public bool Egg { get; set; } = false;
		public bool Wheat { get; set; } = false;
		public bool Soy { get; set; } = false;
		public bool Fish { get; set; } = false;
		public bool Shellfish { get; set; } = false;
		public bool Pollen { get; set; } = false;
		public bool DustMites { get; set; } = false;
		public bool Mold { get; set; } = false;
		public bool PetDander { get; set; } = false;
		public bool InsectStings { get; set; } = false;
		public bool Penicillin { get; set; } = false;
		public bool Aspirin { get; set; } = false;
		public bool NSAIDs { get; set; } = false;
		public bool SulfaDrugs { get; set; } = false;
		public bool Latex { get; set; } = false;
		public bool Fragrances { get; set; } = false;
		public bool Nickel { get; set; } = false;
		public bool Preservatives { get; set; } = false;
	}
}
