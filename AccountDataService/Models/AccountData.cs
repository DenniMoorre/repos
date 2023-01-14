using System.ComponentModel.DataAnnotations;

namespace AccountDataService.Models
{
    public class AccountData
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string DateOfBirth { get; set; }

        [Required]
        public string ResidenceAddress { get; set; }

        [Required]
        public int PassportId { get; set; }

        public Passport Passport {get; set;}
    }
}