using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AthlosifyMobileApp.Models
{
	public class ActivityValidator : AbstractValidator<Activity>
	{
		public ActivityValidator()
		{
			RuleFor(c => c.Name).Must(n => ValidateStringEmpty(n)).WithMessage("Name should not be empty.");
			RuleFor(c => c.CategoryId).NotNull();
			RuleFor(c => c.Description).Must(d => ValidateStringEmpty(d)).WithMessage("Description should not be empty.");
			RuleFor(c => c.NoOfMinutes).GreaterThan(0);
		}

		bool ValidateStringEmpty(string stringValue)
		{
			if (!string.IsNullOrEmpty(stringValue))
				return true;
			return false;
		}
	}
}
