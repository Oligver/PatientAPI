using FluentValidation;

namespace PatientAPI.Models
{
    /// <summary>
    /// Additional information about patient name
    /// </summary>
    public class PatientName : BaseModel
    {
        /// <summary>
        /// Use
        /// </summary>
        public string Use { get; set; }

        /// <summary>
        /// Family
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// Given
        /// </summary>
        public string[] Given { get; set; }
    }

    /// <summary>
    /// Validator for patient name 
    /// </summary>
	public class PatientNameValidator : AbstractValidator<PatientName>
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member       
		public PatientNameValidator()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
		{
			RuleFor(x => x.Family).NotNull();
        }
    }
}
