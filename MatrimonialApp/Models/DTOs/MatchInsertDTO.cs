using System.ComponentModel.DataAnnotations;

namespace MatrimonialApp.Models.DTOs
{
    public class MatchInsertDTO
    {
        [Required(ErrorMessage = "User ID 2 is required.")]
        public int UserID2 { get; set; }

    }
}
