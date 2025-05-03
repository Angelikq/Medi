using System.ComponentModel.DataAnnotations;

namespace Medi.Server.Models.Enities
{
    public class DictionaryBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
