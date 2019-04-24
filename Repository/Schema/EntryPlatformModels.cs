using System;

namespace Repository.Schema 
{
   public class EntryPlatformModel
   {
       public int Id { get; set; }
       public int EntryId { get; set; }
       public int PlatformId { get; set; }
       public string Description { get; set; }
   }
}
