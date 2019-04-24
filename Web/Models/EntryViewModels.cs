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

        [Display(Name = "Function")]
        public string LexiconFunction { get; set; }

        public string Recommendation { get; set; }

        public string Notes { get; set; }
    }

    public class EntryPlatformViewModel : BaseModel
    {
        public int EntryId { get; set; }

        [Display(Name = "Platform")]
        public int PlatformId { get; set; }
    }
}
