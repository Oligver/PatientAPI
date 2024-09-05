using PatientAPI.Models;

namespace PatientAPI.Services
{
	/// <summary>
	/// Interface for patient service
	/// </summary>
    public interface IPatientService
    {
		/// <summary>
		/// Get all patients
		/// </summary>
		Task<List<Patient>> GetAllPatients();

		/// <summary>
		/// Get patient by ID
		/// </summary>
		Task<Patient> GetPatientById(Guid id);

		/// <summary>
		/// Get Patients By Date
		/// </summary>
		Task<List<Patient>> GetPatientsByDate(string dateParameter);

		/// <summary>
		/// Add new patient
		/// </summary>
		Task AddPatient(Patient newPatient);

		/// <summary>
		/// Update patient
		/// </summary>
		Task UpdatePatient(Guid id, Patient updatedPatient);

		/// <summary>
		/// Delete patient
		/// </summary>
		Task DeletePatient(Guid id);
	}
}
