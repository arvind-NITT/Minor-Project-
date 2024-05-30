using System.ComponentModel.DataAnnotations;

namespace MatrimonialApp.Models.DTOs
{
   
    public class ProfileDTO
    {
        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [EnumDataType(typeof(MaritalStatus), ErrorMessage = "Invalid MaritalStatus value.")]
        public MaritalStatus MaritalStatus { get; set; }
        [Range(0, 300, ErrorMessage = "Height must be between 0 and 300.")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "Education is required.")]
        public string Education { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Income must be a positive number.")]
        public decimal Income { get; set; }

        [Required(ErrorMessage = "Religion is required.")]
        public string Religion { get; set; }

        [Required(ErrorMessage = "Caste is required.")]
        public string Caste { get; set; }

        [Required(ErrorMessage = "Mother tongue is required.")]
        public string MotherTongue { get; set; }
        public string Interests { get; set; }
        public string PartnerExpectations { get; set; }
    }
}