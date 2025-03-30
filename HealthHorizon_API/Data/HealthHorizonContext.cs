using HealthHorizon_API.Models.Entities;
using HealthHorizon_API.Models.Medical_Record_Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Data
{
	public class HealthHorizonContext : IdentityDbContext<IdentityUser>
	{
		public DbSet<AIChatLog> AIChatLogs { get; set; }
		public DbSet<Bill> Bills { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
        public DbSet<DoctorSlot> DoctorSlots { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Prescription> Prescriptions { get; set; }
		public DbSet<Staff> StaffMembers { get; set; }
		public DbSet<StaffRole> StaffRoles { get; set; }
		public DbSet<MedicalRecord> MedicalRecords { get; set; }

		public DbSet<AllergyTest> AllergyTests { get; set; }
		public DbSet<BodyMeasurement> BodyMeasurements { get; set; }
		public DbSet<CardiacTest> CardiacTests { get; set; }
		public DbSet<EndocrineTest> EndocrineTests { get; set; }
		public DbSet<GeneticTest> GeneticTests { get; set; }
		public DbSet<ImagingReport> ImagingReports { get; set; }
		public DbSet<InfectiousDiseaseTest> InfectiousDiseaseTests { get; set; }
		public DbSet<LaboratoryTest> LaboratoryTests { get; set; }
		public DbSet<NeurologicalTest> NeurologicalTests { get; set; }
		public DbSet<RespiratoryTest> RespiratoryTests { get; set; }
		public DbSet<VitalSign> VitalSigns { get; set; }

        public HealthHorizonContext(DbContextOptions<HealthHorizonContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Patient>()
				.HasOne(p => p.Address)
				.WithOne(a => a.Patient)
				.HasForeignKey<Patient>(p => p.AddressId)
				.IsRequired(false);

            // Define any relationships, keys, or constraints
            modelBuilder.Entity<DoctorAvailability>()
				.HasKey(d => d.DoctorAvailabilityId);

            modelBuilder.Entity<Staff>()
				.HasOne(s => s.Role)
				.WithMany()
				.HasForeignKey(s => s.RoleId);

			modelBuilder.Entity<AIChatLog>()
				.HasOne(ai => ai.Patient)
				.WithOne()
				.HasForeignKey<AIChatLog>(ai => ai.PatientId);

			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Doctor)
				.WithMany()
				.HasForeignKey(a => a.DoctorId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Patient)
				.WithMany()
				.HasForeignKey(a => a.PatientId)
				.OnDelete(DeleteBehavior.Restrict);

            // Define Appointment to DoctorSlot relationship (newly added)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.DoctorSlot)  // Appointment references DoctorSlot
                .WithMany()                 // One DoctorSlot can have many Appointments
                .HasForeignKey(a => a.DoctorSlotId) // Foreign key in Appointment
                .OnDelete(DeleteBehavior.Restrict);  // Decide on delete behavior, can also be Cascade or SetNull if needed

            modelBuilder.Entity<Bill>()
				.HasOne(b => b.Appointment)
				.WithOne()
				.HasForeignKey<Bill>(b => b.AppointmentId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Prescription>()
				.HasOne(p => p.Appointment)
				.WithOne()
				.HasForeignKey<Prescription>(p => p.AppointmentId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.AllergyTests)
				.WithOne(at => at.MedicalRecord)
				.HasForeignKey(at => at.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.BodyMeasurements)
				.WithOne(bm => bm.MedicalRecord)
				.HasForeignKey(bm => bm.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.CardiacTests)
				.WithOne(ct => ct.MedicalRecord)
				.HasForeignKey(ct => ct.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.EndocrineTests)
				.WithOne(et => et.MedicalRecord)
				.HasForeignKey(et => et.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.GeneticTests)
				.WithOne(gt => gt.MedicalRecord)
				.HasForeignKey(gt => gt.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.ImagingReports)
				.WithOne(ir => ir.MedicalRecord)
				.HasForeignKey(ir => ir.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.InfectiousDiseaseTests)
				.WithOne(idt => idt.MedicalRecord)
				.HasForeignKey(idt => idt.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.LaboratoryTests)
				.WithOne(lt => lt.MedicalRecord)
				.HasForeignKey(lt => lt.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.NeurologicalTests)
				.WithOne(nt => nt.MedicalRecord)
				.HasForeignKey(nt => nt.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.RespiratoryTests)
				.WithOne(rt => rt.MedicalRecord)
				.HasForeignKey(rt => rt.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.VitalSigns)
				.WithOne(vs => vs.MedicalRecord)
				.HasForeignKey(vs => vs.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}