using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Models
{
    public class LexiconEntryBusinessModel
    {
        public List<CategoryBusinessModel> Category { get; set; }
        public List<PlatformBusinessModel> Platform { get; set; }
        public List<SubCategoryBusinessModel> SubCategory { get; set; }
        public List<LexiconEntryTypeBusinessModel> LexiconEntryType { get; set; }

        public LexiconEntryBusinessModel()
        {
            Category = new List<CategoryBusinessModel>();
            Platform = new List<PlatformBusinessModel>();
            SubCategory = new List<SubCategoryBusinessModel>();
            LexiconEntryType = new List<LexiconEntryTypeBusinessModel>();
        }

        //grantwinney.com/how-to-compare-two-objects-testing-for-equality-in-c/
        //stackoverflow.com/questions/12795882/quickest-way-to-compare-two-list

        public override int GetHashCode()
        {
            return Category.GetHashCode()
                ^ Platform.GetHashCode()
                ^ SubCategory.GetHashCode()
                ^ LexiconEntryType.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other is LexiconEntryBusinessModel)
            {
                var that = other as LexiconEntryBusinessModel;

                // TODO
                // what happens if the `that` has a higher count of items?
                // look at also comparing the count / if the one has more than the other first

                return !Category.Except(that.Category).ToList().Any()
                    && !Platform.Except(that.Platform).ToList().Any()
                    && !SubCategory.Except(that.SubCategory).ToList().Any()
                    && !LexiconEntryType.Except(that.LexiconEntryType).ToList().Any();
            }

            return false;
        }
    }
}
