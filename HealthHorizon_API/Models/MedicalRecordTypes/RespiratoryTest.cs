﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HealthHorizon_API.Models.Entities;

namespace HealthHorizon_API.Models.Medical_Record_Types
{
	public class RespiratoryTest
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime Date { get; set; }

		public Guid DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;

		public Guid MedicalRecordId { get; set; }
		public MedicalRecord MedicalRecord { get; set; } = null!;

		public bool SpirometryPerformed { get; set; } = false;
		public float FEV1 { get; set; } = 0;
		public float FVC { get; set; } = 0;
		public float FEV1FVC { get; set; } = 0;

		public bool PeakFlowTest { get; set; } = false;
		public float PeakFlowRate { get; set; } = 0;

		public bool BloodGasTest { get; set; } = false;
		public float OxygenSaturation { get; set; } = 0;
		public float ArterialPaO2 { get; set; } = 0;
		public float ArterialPaCO2 { get; set; } = 0;
		public float BloodPH { get; set; } = 0;

		public bool ChestXRayPerformed { get; set; } = false;
		public string ChestXRayResults { get; set; } = string.Empty;

		public bool CTScanPerformed { get; set; } = false;
		public string CTScanResults { get; set; } = string.Empty;

		public bool BronchoscopyPerformed { get; set; } = false;
		public string BronchoscopyResults { get; set; } = string.Empty;

		public string TestLab { get; set; } = string.Empty;
		public string Notes { get; set; } = string.Empty;
	}
}
