using System.Collections.Generic;
using Repository.Implementation;
using Repository.Interface;
using Repository.Schema;

namespace Repository.MsSQL
{
   public class PlatformRepository : MsSQLContext, IPlatformRepository
   {
       private readonly string _connectionString;
        public PlatformRepository(string connectionString) : base(connectionString)
        {
            // Shim for BulkInsert
            _connectionString = connectionString;
        }

        public PlatformModel Select(int id)
        {
           var storedProc = "sp_select_platform";
           return Select<PlatformModel>(storedProc, new { id });
        }

      public List<PlatformModel> SelectList()
      {
           var storedProc = "sp_selectlist_platform";
           return SelectList<PlatformModel>(storedProc);
      }

      public int Insert(PlatformModel obj)
      {
           var storedProc = "sp_insert_platform";
           var insertObj = new
           {
                description = obj.Description
           };
           return Insert(storedProc, insertObj);
      }

      public void InsertBulk(List<PlatformModel> listPoco)
      {
         foreach (var obj in listPoco)
         {
            // sweet hack, although a new connection per insert will probably be used -_- perhaps it will pool? meh :D
            // probably better to just have the sql command text in the code for a bulk insert
            new PlatformRepository(_connectionString).Insert(obj);
         }
      }

      public void Update(PlatformModel obj)
      {
           var storedProc = "sp_update_platform";
           var updateObj = new
           {
                id = obj.Id,
                description = obj.Description
           };
           Update(storedProc, updateObj);
      }

      public void Delete(int id)
      {
           var storedProc = "sp_delete_platform";
           Delete(storedProc, id);
      }
   }
}
