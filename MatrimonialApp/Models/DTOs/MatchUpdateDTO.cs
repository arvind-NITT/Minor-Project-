using System.ComponentModel.DataAnnotations;

namespace MatrimonialApp.Models.DTOs
{
    public class MatchUpdateDTO
    {
        [Required(ErrorMessage = "User ID 2 is required.")]
        public int UserID2 { get; set; }

        [EnumDataType(typeof(MatchStatus), ErrorMessage = "Invalid MatchStatus value.")]
        public MatchStatus MatchStatus { get; set; }

    }
}
