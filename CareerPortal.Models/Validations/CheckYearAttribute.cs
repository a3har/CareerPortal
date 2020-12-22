using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CareerPortal.Models.Validations
{
    public class CheckYearBetweenAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _startYear;
        private readonly int _endYear;

        public CheckYearBetweenAttribute(int startYear,int endYear=0)
        {
            
            if (endYear == 0)
            {
                _endYear = Int32.Parse(DateTime.Now.Year.ToString());
            }
            if (endYear < startYear)
            {
                _startYear = endYear;
                _endYear = startYear;
            }
            else
            {
                _startYear = startYear;
                _endYear = endYear;
            }
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            var error = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-error", error);
        }

        public override bool IsValid(object value)
        {
            int val = Int32.Parse(value.ToString());
            return val < _endYear && val > _startYear;
        }
    }
}
