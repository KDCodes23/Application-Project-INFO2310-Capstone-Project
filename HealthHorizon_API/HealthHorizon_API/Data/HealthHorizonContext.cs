using HealthHorizon_API.Models;
using HealthHorizon_API.Models.Medical_Record_Types;
using HealthHorizon_API.Models.PersonTypes;
using Microsoft.EntityFrameworkCore;

namespace HealthHorizon_API.Data
{
	public class HealthHorizonContext : DbContext
	{
		public DbSet<AIChatLog> AIChatLogs { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Bill> Bills { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Feedback> Feedbacks { get; set; }
		public DbSet<MedicalRecord> MedicalRecords { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Prescription> Prescriptions { get; set; }
		public DbSet<Staff> Staff { get; set; }
		public DbSet<StaffRole> Roles { get; set; }

		public HealthHorizonContext(DbContextOptions<HealthHorizonContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AIChatLog>().HasOne(ai => ai.Patient).WithMany().HasForeignKey(ai => ai.PatientId);

			modelBuilder.Entity<Appointment>().HasOne(a => a.Doctor).WithMany().HasForeignKey(a => a.DoctorId);
			modelBuilder.Entity<Appointment>().HasOne(a => a.Patient).WithMany().HasForeignKey(a => a.PatientId);

			modelBuilder.Entity<Bill>().HasOne(b => b.Appointment).WithMany().HasForeignKey(a => a.AppointmentId);

			modelBuilder.Entity<MedicalRecord>().HasOne(a => a.Patient).WithMany().HasForeignKey(a => a.PatientId);

			modelBuilder.Entity<Prescription>().HasOne(b => b.Appointment).WithMany().HasForeignKey(a => a.AppointmentId);

			modelBuilder.Entity<Staff>().HasOne(b => b.Role).WithMany().HasForeignKey(a => a.RoleId);

			modelBuilder.Entity<Patient>().OwnsOne(p => p.Address);

			modelBuilder.Entity<MedicalRecord>().OwnsMany(mr => mr.AllergyTests, at =>
			{
				at.WithOwner();
				at.HasOne(at => at.Doctor).WithMany().HasForeignKey(at => at.DoctorId);
			});

			modelBuilder.Entity<MedicalRecord>().OwnsMany(mr => mr.BodyMeasurements, bm =>
			{
				bm.WithOwner();
				bm.HasOne(bm => bm.Doctor).WithMany().HasForeignKey(bm => bm.DoctorId);
			});

			modelBuilder.Entity<MedicalRecord>().OwnsMany(mr => mr.CardiacTests, ct =>
			{
				ct.WithOwner();
				ct.HasOne(ct => ct.Doctor).WithMany().HasForeignKey(ct => ct.DoctorId);
			});

			modelBuilder.Entity<MedicalRecord>().OwnsMany(mr => mr.EndocrineTests, et =>
			{
				et.WithOwner();
				et.HasOne(et => et.Doctor).WithMany().HasForeignKey(et => et.DoctorId);
			});

			modelBuilder.Entity<MedicalRecord>().OwnsMany(mr => mr.GeneticTests, gt =>
			{
				gt.WithOwner();
				gt.HasOne(gt => gt.Doctor).WithMany().HasForeignKey(gt => gt.DoctorId);
			});

			modelBuilder.Entity<MedicalRecord>().OwnsMany(mr => mr.ImagingReports, ir =>
			{
				ir.WithOwner();
				ir.HasOne(ir => ir.Doctor).WithMany().HasForeignKey(ir => ir.DoctorId);
			});

			modelBuilder.Entity<MedicalRecord>().OwnsMany(mr => mr.InfectiousDiseaseTests, idt =>
			{
				idt.WithOwner();
				idt.HasOne(idt => idt.Doctor).WithMany().HasForeignKey(idt => idt.DoctorId);
			});

			modelBuilder.Entity<MedicalRecord>().OwnsMany(mr => mr.LaboratoryTests, lt =>
			{
				lt.WithOwner();
				lt.HasOne(lt => lt.Doctor).WithMany().HasForeignKey(lt => lt.DoctorId);
			});

			modelBuilder.Entity<MedicalRecord>().OwnsMany(mr => mr.NeurologicalTests, nt =>
			{
				nt.WithOwner();
				nt.HasOne(nt => nt.Doctor).WithMany().HasForeignKey(nt => nt.DoctorId);
			});

			modelBuilder.Entity<MedicalRecord>().OwnsMany(mr => mr.RespiratoryTests, rt =>
			{
				rt.WithOwner();
				rt.HasOne(rt => rt.Doctor).WithMany().HasForeignKey(rt => rt.DoctorId);
			});

			modelBuilder.Entity<MedicalRecord>().OwnsMany(mr => mr.VitalSigns, vs =>
			{
				vs.WithOwner();
				vs.HasOne(vs => vs.Doctor).WithMany().HasForeignKey(vs => vs.DoctorId);
			});
		}
	}
}
