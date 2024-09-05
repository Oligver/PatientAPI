using AutoMapper;
using PatientAPI.Models;

namespace PatientAPI.Mapping
{
	/// <summary>
	/// Mapping profile for automapper
	/// </summary>
	public class MappingProfile : Profile
	{
		/// <summary>
		/// 
		/// </summary>
		public MappingProfile()
		{
			CreateMap<Patient, PatientDto>()
				.ReverseMap();
		}
	}
}
