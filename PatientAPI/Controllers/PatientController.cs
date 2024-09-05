using Microsoft.AspNetCore.Mvc;
using PatientAPI.Models;
using System.ComponentModel.DataAnnotations;
using PatientAPI.Services;

namespace PatientAPI.Controllers
{
	/// <summary>
	/// Controller for Patients
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public class PatientController : ControllerBase
	{
		private readonly IPatientService _patientService;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		public PatientController(IPatientService patientService)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
		{
			_patientService = patientService;
		}

		/// <summary>
		/// Get all patients
		/// </summary>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Patient>))]
		public async Task<IActionResult> GetAllPatients()
		{
			var result = await _patientService.GetAllPatients();
			return Ok(result);
		}

		/// <summary>
		/// Get patient by ID
		/// </summary>
		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Patient>))]
		public async Task<IActionResult> GetPatientById([FromRoute, Required] Guid id)
		{
			var result = await _patientService.GetPatientById(id);
			return Ok(result);
		}

		/// <summary>
		/// Get all patients
		/// </summary>
		[HttpGet]
		[Route("birthDate={searchPattern}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Patient>))]
		public async Task<IActionResult> GetAllPatientsByBirthDate([FromRoute, Required] string searchPattern)
		{
			var result = await _patientService.GetPatientsByDate(searchPattern);
			return Ok(result);
		}

		/// <summary>
		/// Add patient
		/// </summary>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> AddPatient([FromBody, Required] Patient request)
		{
			await _patientService.AddPatient(request);
			return Ok();
		}

		/// <summary>
		/// Update patient
		/// </summary>
		[HttpPut]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdatePatient([FromRoute] Guid id, [FromBody, Required] Patient request)
		{
			await _patientService.UpdatePatient(id, request);
			return Ok();
		}

		/// <summary>
		/// Delete patient
		/// </summary>
		[HttpDelete]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeletePatient([FromRoute] Guid id)
		{
			await _patientService.DeletePatient(id);
			return NoContent();
		}
	}
}
