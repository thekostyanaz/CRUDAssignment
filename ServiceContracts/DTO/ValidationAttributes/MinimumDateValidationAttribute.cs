using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.ValidationAttributes
{
	public class MinimumDateValidationAttribute : ValidationAttribute
	{
		private readonly DateTime? _fromDate;

		public MinimumDateValidationAttribute() { }

		public MinimumDateValidationAttribute(string fromDate) 
		{
			_fromDate = DateTime.Parse(fromDate);
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value != null) 
			{
				var date = (DateTime)value;
				if (_fromDate > date) 
				{
					return new ValidationResult($"Date And Time Of Order should be later then {_fromDate.Value.ToString("yyyy-MM-dd")}");
				}
				return ValidationResult.Success;
			}
			return null;
		}
	}
}
