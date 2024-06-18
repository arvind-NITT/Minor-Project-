using System.ComponentModel.DataAnnotations;
using MatrimonialApp.Models;
namespace MatrimonialApp.Models
{
    public class PricingPlan
    {
        [Key]
        public int PricingPlanId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [EnumDataType(typeof(SubscriptionType))]
        public SubscriptionType Type { get; set; }

        public string Description { get; set; }
    }
}
