using HealthHorizon_API.Models;
using HealthHorizon_API.Models.Medical_Record_Types;
using HealthHorizon_API.Models.PersonTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Data
{
	public class HealthHorizonContext : IdentityDbContext<IdentityUser>
	{
		public DbSet<AIChatLog> AIChatLogs { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Bill> Bills { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Feedback> Feedbacks { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Prescription> Prescriptions { get; set; }
		public DbSet<Staff> Staffs { get; set; }
		public DbSet<StaffRole> StaffRoles { get; set; }
		public DbSet<TimeSlot> TimeSlots { get; set; }
		public DbSet<MedicalRecord> MedicalRecords { get; set; }
		public DbSet<AllergyTest> AllergyTests { get; set; }
		public DbSet<BodyMeasurement> BodyMeasurements { get; set; }
		public DbSet<CardiacTest> CardiacTests { get; set; }
		public DbSet<EndocrineTest> EndocrineTests { get; set; }
		public DbSet<GeneticTest> GeneticTests { get; set; }
		public DbSet<ImagingReport> ImagingReports { get; set; }
		public DbSet<InfectiousDiseaseTest> InfectiousDiseaseTests { get; set; }
		public DbSet<LaboratoryTest> LaboratoryTests { get; set; }
		public DbSet<NeurologicalTest> NeurologicalTests{ get; set; }
		public DbSet<RespiratoryTest> RespiratoryTests { get; set; }
		public DbSet<VitalSign> VitalSigns { get; set; }

		public HealthHorizonContext(DbContextOptions<HealthHorizonContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
			modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
			modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

			modelBuilder.Entity<Patient>().HasOne(p => p.Address).WithOne().HasForeignKey<Patient>(p => p.AddressId);

			modelBuilder.Entity<Staff>().HasOne(s => s.Role).WithOne().HasForeignKey<Staff>(s => s.RoleId);

			modelBuilder.Entity<AIChatLog>().HasOne(ai => ai.Patient).WithOne().HasForeignKey<AIChatLog>(ai => ai.PatientId);

			modelBuilder.Entity<Appointment>().HasOne(a => a.Doctor).WithOne().HasForeignKey<Appointment>(a => a.DoctorId);
			modelBuilder.Entity<Appointment>().HasOne(a => a.Patient).WithOne().HasForeignKey<Appointment>(a => a.PatientId);

			modelBuilder.Entity<Bill>().HasOne(b => b.Appointment).WithOne().HasForeignKey<Bill>(b => b.AppointmentId);

			modelBuilder.Entity<Prescription>().HasOne(p => p.Appointment).WithOne().HasForeignKey<Prescription>(p => p.AppointmentId);

			modelBuilder.Entity<TimeSlot>().HasOne(ts => ts.Doctor).WithOne().HasForeignKey<TimeSlot>(ts => ts.DoctorId);

			modelBuilder.Entity<MedicalRecord>().HasMany(mr => mr.AllergyTests).WithOne(at => at.MedicalRecord).HasForeignKey(at => at.MedicalRecordId);
			modelBuilder.Entity<MedicalRecord>().HasMany(mr => mr.BodyMeasurements).WithOne(bm => bm.MedicalRecord).HasForeignKey(bm => bm.MedicalRecordId);
			modelBuilder.Entity<MedicalRecord>().HasMany(mr => mr.CardiacTests).WithOne(ct => ct.MedicalRecord).HasForeignKey(ct => ct.MedicalRecordId);
			modelBuilder.Entity<MedicalRecord>().HasMany(mr => mr.EndocrineTests).WithOne(et => et.MedicalRecord).HasForeignKey(et => et.MedicalRecordId);
			modelBuilder.Entity<MedicalRecord>().HasMany(mr => mr.GeneticTests).WithOne(gt => gt.MedicalRecord).HasForeignKey(gt => gt.MedicalRecordId);
			modelBuilder.Entity<MedicalRecord>().HasMany(mr => mr.ImagingReports).WithOne(ir => ir.MedicalRecord).HasForeignKey(ir => ir.MedicalRecordId);
			modelBuilder.Entity<MedicalRecord>().HasMany(mr => mr.InfectiousDiseaseTests).WithOne(idt => idt.MedicalRecord).HasForeignKey(idt => idt.MedicalRecordId);
			modelBuilder.Entity<MedicalRecord>().HasMany(mr => mr.LaboratoryTests).WithOne(lt => lt.MedicalRecord).HasForeignKey(lt => lt.MedicalRecordId);
			modelBuilder.Entity<MedicalRecord>().HasMany(mr => mr.NeurologicalTests).WithOne(nt => nt.MedicalRecord).HasForeignKey(nt => nt.MedicalRecordId);
			modelBuilder.Entity<MedicalRecord>().HasMany(mr => mr.RespiratoryTests).WithOne(rt => rt.MedicalRecord).HasForeignKey(rt => rt.MedicalRecordId);
			modelBuilder.Entity<MedicalRecord>().HasMany(mr => mr.VitalSigns).WithOne(vs => vs.MedicalRecord).HasForeignKey(vs => vs.MedicalRecordId);
		}
	}
}
