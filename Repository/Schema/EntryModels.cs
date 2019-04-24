namespace Repository.Schema 
{
   public class EntryModel
   {
       public int Id { get; set; }
       public int CategoryId { get; set; }
       public int SubCategoryId { get; set; }
       public string LexiconFunction { get; set; }
       public string Recommendation { get; set; }
       public string Notes { get; set; }
   }
}
