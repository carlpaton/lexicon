using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class EntryPlatformViewModel
    {
        public EntryPlatformViewModel()
        {
            Entry = new EntryViewModel();
            EntryPlatformList = new List<EntryPlatform>();
        }
        public int Id { get; set; }

        public EntryViewModel Entry { get; set; }
        
        public List<EntryPlatform> EntryPlatformList { get; set; }
    } 

    public class EntryPlatform : BaseModel
    { 
        public int EntryId { get; set; }

        [Display(Name = "Platform")]
        public int PlatformId { get; set; }
    }
}