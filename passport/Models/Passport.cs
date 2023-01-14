using System.ComponentModel.DataAnnotations;

namespace passport.Models
{
    public class Passport
    {


        [Key]
        [Required]
        public int Id { get; set; }


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
