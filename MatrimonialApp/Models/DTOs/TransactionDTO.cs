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
        [MaxLength(50)]
        public string TransactionType { get; set; } // E.g., "Upgrade to Premium"
    }
}
