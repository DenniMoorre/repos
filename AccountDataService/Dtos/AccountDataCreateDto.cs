using System.ComponentModel.DataAnnotations;

namespace AccountDataService.Dtos
{
    public class AccountDataCreateDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}