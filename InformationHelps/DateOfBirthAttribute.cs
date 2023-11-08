using System;
using System.ComponentModel.DataAnnotations;

namespace backendTask.InformationHelps
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        public DateOfBirthAttribute()
        {
            ErrorMessage = "Неверная дата рождения. Вам должно быть не менее 13 лет и не более 100 лет.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is DateTime dateOfBirth)
            {
                DateTime now = DateTime.Now;
                int age = now.Year - dateOfBirth.Year;

                if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                {
                    age--;
                }

                if (age >= 13 && age <= 100)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}