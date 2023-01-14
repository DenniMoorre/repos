using System.ComponentModel.DataAnnotations;

namespace passport.Dtos
{
    public class PassportCreateDto
    {


        [Required]
        public int PassportNumber { get; set; }


        [Required]
        public string AuthorityThatIssued { get; set; }


        [Required]
        public string Status { get; set; }



        [Required]
        public string AccountData { get; set; }

    }
}