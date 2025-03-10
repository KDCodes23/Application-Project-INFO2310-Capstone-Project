﻿using HealthHorizon_API.Models.PersonTypes;

namespace HealthHorizon_API.Models.Medical_Record_Types
{
	public class CardiacTest
	{
		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public bool Electrocardiogram { get; set; } = false;
		public bool Echocardiogram { get; set; } = false;
		public bool StressTest { get; set; } = false;
		public bool HolterMonitor { get; set; } = false;
		public bool EventMonitor { get; set; } = false;
		public bool CardiacMRI { get; set; } = false;
		public bool CardiacCT { get; set; } = false;
		public bool CoronaryAngiogram { get; set; } = false;
		public bool CalciumScoreTest { get; set; } = false;
		public bool BloodPressureMonitoring { get; set; } = false;

		public int RestingHeartRate { get; set; } = 0;
		public int MaxHeartRate { get; set; } = 0;

		public double SystolicBP { get; set; } = 0;
		public double DiastolicBP { get; set; } = 0;
		public double CholesterolLevel { get; set; } = 0;
		public double TriglycerideLevel { get; set; } = 0;
		public double LDLCholesterol { get; set; } = 0;
		public double HDLCholesterol { get; set; } = 0;
		public double EjectionFraction { get; set; } = 0;
		public double CoronaryCalciumScore { get; set; } = 0;
	}
}
