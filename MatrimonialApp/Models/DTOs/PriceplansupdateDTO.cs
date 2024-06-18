using System.ComponentModel.DataAnnotations;

namespace MatrimonialApp.Models.DTOs
{
    public class PriceplansupdateDTO
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        [EnumDataType(typeof(SubscriptionType))]
        public SubscriptionType Type { get; set; }

        public string Description { get; set; }
    }
}
