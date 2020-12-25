using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CareerPortal.Models.Validations
{
    public class YearLessThanAttribute : ValidationAttribute 
    { 
        private readonly string _comparisonProperty;

        public YearLessThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var currentValue = (Int32)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (Int32)property.GetValue(validationContext.ObjectInstance);

            if (currentValue > comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }

        //public void AddValidation(ClientModelValidationContext context)
        //{
        //    var error = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
        //    context.Attributes.Add("data-val", "true");
        //    context.Attributes.Add("data-val-error", error);
        //}
    }
}
