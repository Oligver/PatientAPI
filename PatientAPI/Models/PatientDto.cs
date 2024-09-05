using FluentValidation;

namespace PatientAPI.Models
{
    /// <summary>
    /// Patient information from DB
    /// </summary>
    public class PatientDto : BaseModel
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

    /// <summary>
    /// Validator for Patient dto
    /// </summary>
    public class PatientValidator : AbstractValidator<PatientDto>
    {
        private readonly List<string> _genderList = new() { "male", "female", "other", "unknown" };

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member       
		public PatientValidator()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
		{
			RuleFor(x => x.BirthDate).NotNull();
            RuleFor(x => x.Gender).NotNull();
            RuleFor(x => x.Gender)
                .Must(x => _genderList.Contains(x.ToLower()))
                .WithMessage($"Use only one of this values: {string.Join(", ", _genderList)}");
        }
    }
}
