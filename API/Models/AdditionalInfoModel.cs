using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class AdditionalInfoModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z]*$")]
        public string FirstName { get; set; }

        [RegularExpression("[a-zA-Z]*$")]
        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$")]
        public int Phone { get; set; }

        [Required]
        public string PaymentType { get; set; }

        [MaxLength(600)]
        public string OrderComment { get; set; }

        public bool AdditionalInfo { get; set; }
    }
}
