using System.ComponentModel.DataAnnotations;

namespace MatrimonialApp.Models.DTOs
{

    public class SubscriptionDTO
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Subscription type is required")]
        public string SubscriptionType { get; set; } // Consider using an enum

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DateGreaterThan("StartDate", ErrorMessage = "End date must be greater than start date")]
        public DateTime EndDate { get; set; }
    }

    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public DateGreaterThanAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            if (startDateProperty == null)
            {
                return new ValidationResult($"Unknown property {_startDatePropertyName}");
            }

            var startDateValue = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance);
            var endDateValue = (DateTime)value;

            if (endDateValue <= startDateValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
