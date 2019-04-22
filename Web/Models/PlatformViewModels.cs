using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PlatformViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
