using System.ComponentModel.DataAnnotations;

namespace MatrimonialApp.Models.DTOs
{

    public class SubscriptionDTO
    {
        //public int UserID { get; set; }

        [EnumDataType(typeof(SubscriptionType))]
        public SubscriptionType Type { get; set; }
        [Required]
        public int TransactionId { get; set; }

    }

   
    
}
