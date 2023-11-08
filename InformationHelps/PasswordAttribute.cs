using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace backendTask.InformationHelps
{
    public class PasswordAttribute : ValidationAttribute
    {
        private static readonly string PasswordRegex = @"^(?=.*[A-Z])(?=.*[a-z]{7,})(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]+$";

        public PasswordAttribute()
        {
            ErrorMessage = "Пароль не соответствует требованиям. Пароль должен содержать минимум 8 символов.";
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return false;
            }

            string password = value.ToString();
            return Regex.IsMatch(password, PasswordRegex) && password.Length >= 8;
        }
    }
}
