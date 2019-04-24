using System.Collections.Generic;
using System.Linq;

namespace Business.Models
{
    public class EntryBusinessModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string LexiconFunction { get; set; }
        public string Recommendation { get; set; }
        public string Notes { get; set; }
        public List<CategoryBusinessModel> Category { get; set; }
        public List<SubCategoryBusinessModel> SubCategory { get; set; }
        public List<EntryPlatformBusinessModel> EntryPlatform { get; set; }

        public EntryBusinessModel()
        {
            Category = new List<CategoryBusinessModel>();
            SubCategory = new List<SubCategoryBusinessModel>();
            EntryPlatform = new List<EntryPlatformBusinessModel>();
        }

        //grantwinney.com/how-to-compare-two-objects-testing-for-equality-in-c/
        //stackoverflow.com/questions/12795882/quickest-way-to-compare-two-list

        public override int GetHashCode()
        {
            return Category.GetHashCode()
                ^ SubCategory.GetHashCode()
                ^ EntryPlatform.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other is EntryBusinessModel)
            {
                var that = other as EntryBusinessModel;

                // TODO
                // what happens if the `that` has a higher count of items?
                // look at also comparing the count / if the one has more than the other first

                return !Category.Except(that.Category).ToList().Any()
                    && !SubCategory.Except(that.SubCategory).ToList().Any()
                    && !EntryPlatform.Except(that.EntryPlatform).ToList().Any();
            }

            return false;
        }
    }
}
