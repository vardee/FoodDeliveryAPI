using System;
using System.ComponentModel.DataAnnotations;

namespace backendTask.InformationHelps.Validator
{
    public class MinDeliveryTimeValidator
    {
        private readonly IConfiguration _configuration;

        public MinDeliveryTimeValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValid(object value)
        {
            if (value is DateTime deliveryTime)
            {
                var currentTime = DateTime.UtcNow;
                var minimumDeliveryTime = currentTime.AddMinutes(double.Parse(_configuration["TimeForDelivery:DeliveryTime"]));
                return deliveryTime >= minimumDeliveryTime;
            }

            return false;
        }
    }
}