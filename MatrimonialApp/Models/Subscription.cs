using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrimonialApp.Models
{
    public class Subscription
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int SubscriptionId { get; set; }

            [Required]
            public int UserId { get; set; }
            [Required]
            public int TransactionId { get; set; }
            [Required]
            [EnumDataType(typeof(SubscriptionType))]
            public SubscriptionType Type { get; set; }

            [Required]
            public DateTime StartDate { get; set; }

            public DateTime? EndDate { get; set; }

            [ForeignKey("UserId")]
            public virtual User User { get; set; }
        }

        public enum SubscriptionType
        {
            Basic,
            Premium
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
