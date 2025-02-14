using HealthHorizon_API.Models;
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
			modelBuilder.Entity<MedicalRecord>().HasOne(a => a.Doctor).WithMany().HasForeignKey(a => a.DoctorId);
			modelBuilder.Entity<MedicalRecord>().HasOne(a => a.Patient).WithMany().HasForeignKey(a => a.PatientId);
			modelBuilder.Entity<Prescription>().HasOne(b => b.Appointment).WithMany().HasForeignKey(a => a.AppointmentId);
			modelBuilder.Entity<Staff>().HasOne(b => b.Role).WithMany().HasForeignKey(a => a.RoleId);
			modelBuilder.Entity<Patient>().OwnsOne(p => p.Address);
		}
	}
}
