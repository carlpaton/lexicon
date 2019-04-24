using Repository.Schema;
using System.Collections.Generic;

namespace Repository.Interface 
{
   public interface IEntryPlatformRepository : IRepository<EntryPlatformModel>
   {
        List<EntryPlatformModel> SelectListByEntryId(int id);
   }
}
