namespace PatientAPI.Models
{
	/// <summary>
	/// Information about patient
	/// </summary>
	public class Patient
	{
		/// <summary>
		/// Name
		/// </summary>
		public PatientName Name { get; set; }

		/// <summary>
		/// Gender
		/// </summary>
		public string Gender { get; set; }

		/// <summary>
		/// BirthDate
		/// </summary>
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// Active
		/// </summary>
		public bool Active { get; set; }
	}
}
