using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrimonialApp.Models.DTOs
{
    public class TransactionDTO
    {
        [Required]
        [MaxLength(50)]
        public string UPIID { get; set; }

        [Required]
        [EnumDataType(typeof(SubscriptionType))]
        public SubscriptionType Type { get; set; }
    }
}
