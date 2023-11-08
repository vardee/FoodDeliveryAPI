using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backendTask.InformationHelps.Validator
{
    public class DateOfBirthValidator
    {
        public static bool ValidateDateOfBirth(DateTime dateOfBirth)
        {

            DateTime now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;

            if (now.Month < dateOfBirth.Month || now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)
            {
                age--;
            }

            if (age < 13 || age > 100)
            {
                return false;
            }

            return true;
        }
    }
}