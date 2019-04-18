namespace Repository.Schema 
{
   public class LexiconEntryModel
   {
       public int Id { get; set; }
       public int CategoryId { get; set; }
       public int PlatformId { get; set; }
       public int SubCategoryId { get; set; }
       public int LexiconEntryTypeId { get; set; }
       public string Description { get; set; }
   }
}
