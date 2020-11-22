using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //we have to get customer instance
            var customer = (Customer)validationContext.ObjectInstance;
            // Id 1-4 null--> not selected

            if(customer.MembershipTypeId == MembershipType.PayAsYouGo || customer.MembershipTypeId == MembershipType.Unknown)
            {
                return ValidationResult.Success;
            }
            if(customer.Birthday == null)
            {
                return new ValidationResult("Birthdate is required");
            }
            var customerAge = DateTime.Today.Year - customer.Birthday.Value.Year;
            if(customerAge >= 18)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Customer should be at least 18 years old to go on a member");
            }
        }
    }
}