using MatrimonialApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MainRequestTrackerAPI.Models.DTOs
{
   
    public class MatchDTO
    {
        //public int UserId { get; set; }

        [Required(ErrorMessage = "Looking for is required.")]
        public string Looking_for { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Age must be a numeric value.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Religion is required.")]
        public string Religion { get; set; }

        [Required(ErrorMessage = "Mother tongue is required.")]
        public string MotherTongue { get; set; }

    }
}
