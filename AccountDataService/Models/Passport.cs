using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountDataService.Models
{
    public class Passport
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExternalID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<AccountData> AccountData { get; set; } = new List<AccountData>();
     }
}