using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class EntryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        [Display(Name = "Sub Category")]
        public int? SubCategoryId { get; set; }

        [Required]
        [Display(Name = "Function")]
        public string LexiconFunction { get; set; }

        [Required]
        public string Recommendation { get; set; }

        public string Notes { get; set; }
    }
}
