using System.Collections.Generic;
using Repository.Implementation;
using Repository.Interface;
using Repository.Schema;

namespace Repository.MsSQL
{
   public class EntryPlatformRepository : MsSQLContext, IEntryPlatformRepository
   {
       private readonly string _connectionString;
        public EntryPlatformRepository(string connectionString) : base(connectionString)
        {
            // Shim for BulkInsert
            _connectionString = connectionString;
        }

        public EntryPlatformModel Select(int id)
        {
           var storedProc = "sp_select_entry_platform";
           return Select<EntryPlatformModel>(storedProc, new { id });
        }

      public List<EntryPlatformModel> SelectList()
      {
           var storedProc = "sp_selectlist_entry_platform";
           return SelectList<EntryPlatformModel>(storedProc);
      }

      public int Insert(EntryPlatformModel obj)
      {
           var storedProc = "sp_insert_entry_platform";
           var insertObj = new
           {
                entry_id = obj.EntryId,
                platform_id = obj.PlatformId,
                description = obj.Description
           };
           return Insert(storedProc, insertObj);
      }

      public void InsertBulk(List<EntryPlatformModel> listPoco)
      {
         foreach (var obj in listPoco)
         {
            // sweet hack, although a new connection per insert will probably be used -_- perhaps it will pool? meh :D
            // probably better to just have the sql command text in the code for a bulk insert
            new EntryPlatformRepository(_connectionString).Insert(obj);
         }
      }

      public void Update(EntryPlatformModel obj)
      {
           var storedProc = "sp_update_entry_platform";
           var updateObj = new
           {
                id = obj.Id,
                entry_id = obj.EntryId,
                platform_id = obj.PlatformId,
                description = obj.Description
           };
           Update(storedProc, updateObj);
      }

      public void Delete(int id)
      {
           var storedProc = "sp_delete_entry_platform";
           Delete(storedProc, id);
      }
   }
}
