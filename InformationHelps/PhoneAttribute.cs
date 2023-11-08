using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace backendTask.InformationHelps
{
    public class PhoneAttribute : ValidationAttribute
    {
        private static readonly string PhoneNumberRegex = @"^[78]\d{9}$";

        public PhoneAttribute()
        {
            ErrorMessage = "Данный формат номера телефона не валиден";
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return true;
            }

            string phoneNumber = value.ToString();
            return Regex.IsMatch(phoneNumber, PhoneNumberRegex);
        }
    }
}
