using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace backendTask.InformationHelps
{
    public class Phone : ValidationAttribute
    {
        private static readonly string PhoneNumberRegex = @"^\+\d{1,3}\s?\d{3,14}$";

        public Phone()
        {
            ErrorMessage = "Invalid phone number format";
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
