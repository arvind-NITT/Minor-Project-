using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrimonialApp.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string TransactionType { get; set; } // E.g., "Upgrade to Premium"
        [Required]
        public bool IsApproved { get; set; }
        [Required]
        [MaxLength(50)]
        public string UPIID { get; set; }


        //[ForeignKey("UserId")]
        //public virtual User User { get; set; }
    }
}
