using System;
using System.ComponentModel.DataAnnotations;

namespace backendTask.InformationHelps
{
    public class MinDeliveryTimeAttribute : ValidationAttribute
    {
        public MinDeliveryTimeAttribute(int minutes)
        {
            Minutes = minutes;
            ErrorMessage = $"Доставка должна быть не менее чем через {minutes} минут.";
        }

        public int Minutes { get; }

        public override bool IsValid(object value)
        {
            if (value is DateTime deliveryTime)
            {
                var currentTime = DateTime.UtcNow;
                var minimumDeliveryTime = currentTime.AddMinutes(Minutes);
                return deliveryTime >= minimumDeliveryTime;
            }

            return false;
        }
    }
}