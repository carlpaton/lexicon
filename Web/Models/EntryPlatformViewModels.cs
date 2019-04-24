using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class EntryPlatformViewModel
    {
        public EntryPlatformViewModel()
        {
            Entry = new EntryViewModel();
            EntryPlatformList = new List<EntryPlatformListViewModel>();
        }

        public EntryViewModel Entry { get; set; }
        
        public List<EntryPlatformListViewModel> EntryPlatformList { get; set; }
    } 

    public class EntryPlatformListViewModel : BaseModel
    { 
        public int EntryId { get; set; }

        [Display(Name = "Platform")]
        public int PlatformId { get; set; }
    }
}