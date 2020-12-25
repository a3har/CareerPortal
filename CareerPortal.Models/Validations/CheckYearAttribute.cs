using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CareerPortal.Models.Validations
{
    public class CheckYearBetweenAttribute : ValidationAttribute
    {
        private int _startYear { get; }
        private int _endYear;

        public CheckYearBetweenAttribute(int startYear)
        {
            
           _endYear = Int32.Parse(DateTime.Now.Year.ToString());
            if (_endYear < startYear)
            {
                _startYear = _endYear;
                _endYear = startYear;
            }
            else
            {
                _startYear = startYear;
            }
        }

        //public void AddValidation(ClientModelValidationContext context)
        //{
        //    var error = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
        //    context.Attributes.Add("data-val", "true");
        //    context.Attributes.Add("data-val-error", error);
        //}

        public override bool IsValid(object value)
        {
            int val = (int) value;
            return val < _endYear && val > _startYear;
        }
    }
}
