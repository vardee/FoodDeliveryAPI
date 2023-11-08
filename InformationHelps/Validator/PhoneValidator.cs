using System.Text.RegularExpressions;

namespace backendTask.InformationHelps.Validator
{
    public static class PhoneValidator
    {
        private static readonly string PhoneNumberRegex = @"^[78]\d{10}$";

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return true;
            }

            return Regex.IsMatch(phoneNumber, PhoneNumberRegex);
        }
    }
}