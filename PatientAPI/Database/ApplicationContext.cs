using Microsoft.EntityFrameworkCore;
using PatientAPI.Models;

namespace PatientAPI.Database
{
    /// <summary>
    /// Context for working with DB
    /// </summary>
    public class ApplicationContext : DbContext
	{
		/// <summary>
		/// Patients
		/// </summary>
		public DbSet<PatientDto> Patients { get; set; }

		/// <summary>
		/// PatientNames (FK)
		/// </summary>
		public DbSet<PatientName> PatientNames { get; set; }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
		{
		}
	}
}
