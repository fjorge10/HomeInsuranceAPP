using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace HomeInsuranceAPP.Extensions.ModelValidation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EmailAtribute : ValidationAttribute, IClientValidatable
    {
        private readonly string emailRegex = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string valueAsString = (string)value;

            if (string.IsNullOrEmpty(valueAsString))
            {
                return ValidationResult.Success;
            }
            else
            {
                Regex regex = new Regex(emailRegex);

                Match match = regex.Match(valueAsString);

                if (!match.Success)
                    return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName));
                else
                    return ValidationResult.Success;
            }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            List<ModelClientValidationRule> rules = new List<ModelClientValidationRule>();

            ModelClientValidationRule rule = new ModelClientValidationRule();

            rule.ErrorMessage = string.Format(ErrorMessageString, metadata.DisplayName);
            rule.ValidationType = "Email";

            rule.ValidationParameters["eamilregex"] = emailRegex;

            rules.Add(rule);

            return rules;
        }
    }
}