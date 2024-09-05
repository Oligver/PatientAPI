using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientAPI.Database;
using PatientAPI.Models;


namespace PatientAPI.Services
{
	/// <summary>
	/// Implementation of IPatientService
	/// </summary>
	public class PatientService: IPatientService
	{
		private readonly ApplicationContext _context;
		private readonly IMapper _mapper;
		private readonly ILogger<PatientService> _logger;

		/// <summary>
		/// Constructor
		/// </summary>
		public PatientService(ApplicationContext context, IMapper mapper, ILogger<PatientService> logger)
		{
			_context = context;
			_mapper = mapper;
			_logger = logger;
		}

		/// <inheritdoc />
		public async Task<List<Patient>> GetAllPatients()
		{
			var patients = await _context.Patients.Include(x => x.Name).ToListAsync();
			return _mapper.Map<List<Patient>>(patients);
		}

		/// <inheritdoc />
		public async Task<Patient> GetPatientById(Guid id)
		{
			var patientDb = await _context.Patients.Include(x => x.Name).FirstOrDefaultAsync(x => x.Name.Id == id);

			return _mapper.Map<Patient>(patientDb);
		}

		/// <inheritdoc />
		public async Task<List<Patient>> GetPatientsByDate(string dateParameter)
		{
			string condition = "eq";
			if (!DateTime.TryParse(dateParameter, out var date))
			{
				condition = dateParameter.Substring(0, 2);
				DateTime.TryParse(dateParameter.Substring(2), out date);
			}

			_logger.LogDebug($"condition:{condition}, date:{date}");

			var allPatients = await _context.Patients.Include(x=>x.Name).ToListAsync();
			return condition.ToLower() switch
			{
				"ap" => _mapper.Map<List<Patient>>(allPatients.Where(x => x.BirthDate.Equals(date)).ToList()),
				"ne" => _mapper.Map<List<Patient>>(allPatients.Where(x => !x.BirthDate.Equals(date)).ToList()),
				"gt" => _mapper.Map<List<Patient>>(allPatients.Where(x => x.BirthDate > date).ToList()),
				"lt" => _mapper.Map<List<Patient>>(allPatients.Where(x => x.BirthDate < date).ToList()),
				"ge" => _mapper.Map<List<Patient>>(allPatients.Where(x => x.BirthDate.Equals(date) || x.BirthDate > date).ToList()),
				"le" => _mapper.Map<List<Patient>>(allPatients.Where(x => x.BirthDate.Equals(date) || x.BirthDate < date).ToList()),
				"sa" => _mapper.Map<List<Patient>>(allPatients.Where(x => x.BirthDate.Date > date).ToList()),
				"eb" => _mapper.Map<List<Patient>>(allPatients.Where(x => x.BirthDate.Date < date).ToList()),
				_ => _mapper.Map<List<Patient>>(allPatients.Where(x => x.BirthDate.Date.Equals(date.Date)).ToList())
			};
		}

		/// <inheritdoc />
		public async Task AddPatient(Patient newPatient)
		{
			var patientDto = _mapper.Map<PatientDto>(newPatient);
			await _context.Patients.AddAsync(patientDto);

			await _context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task UpdatePatient(Guid id, Patient updatedPatient)
		{
			var patientDb = await _context.Patients.Include(x=>x.Name).FirstOrDefaultAsync(x=>x.Name.Id == id);
			
			if (patientDb != null)
			{
				patientDb.Active = updatedPatient.Active;
				patientDb.BirthDate = updatedPatient.BirthDate;
				patientDb.Gender = updatedPatient.Gender;
				patientDb.Name.Family = updatedPatient.Name.Family;
				patientDb.Name.Use = updatedPatient.Name.Use;
				patientDb.Name.Given = updatedPatient.Name.Given;

				await _context.SaveChangesAsync();
			}
		}

		/// <inheritdoc />
		public async Task DeletePatient(Guid id)
		{
			var patientDb = await _context.Patients.Include(x => x.Name).FirstOrDefaultAsync(x => x.Name.Id == id);

			if (patientDb != null)
			{
				_context.Patients.Remove(patientDb);
				await _context.SaveChangesAsync();
			}
		}
	}
}
