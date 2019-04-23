﻿using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class LexiconEntryViewModel : BaseModel
    {
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        [Display(Name = "Platform")]
        public int PlatformId { get; set; }

        [Display(Name = "Sub Category")]
        public int? SubCategoryId { get; set; }

        [Display(Name = "Entry Type")]
        public int LexiconEntryTypeId { get; set; }
    }

    public class LexiconEntryTypeViewModel : BaseModel
    {

    }
}
