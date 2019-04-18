using System.Collections.Generic;
using Repository.Implementation;
using Repository.Interface;
using Repository.Schema;

namespace Repository.MsSQL
{
   public class SubCategoryRepository : MsSQLContext, ISubCategoryRepository
   {
       private readonly string _connectionString;
        public SubCategoryRepository(string connectionString) : base(connectionString)
        {
            // Shim for BulkInsert
            _connectionString = connectionString;
        }

        public SubCategoryModel Select(int id)
        {
           var storedProc = "sp_select_sub_category";
           return Select<SubCategoryModel>(storedProc, new { id });
        }

      public List<SubCategoryModel> SelectList()
      {
           var storedProc = "sp_selectlist_sub_category";
           return SelectList<SubCategoryModel>(storedProc);
      }

      public int Insert(SubCategoryModel obj)
      {
           var storedProc = "sp_insert_sub_category";
           var insertObj = new
           {
                description = obj.Description
           };
           return Insert(storedProc, insertObj);
      }

      public void InsertBulk(List<SubCategoryModel> listPoco)
      {
         foreach (var obj in listPoco)
         {
            // sweet hack, although a new connection per insert will probably be used -_- perhaps it will pool? meh :D
            // probably better to just have the sql command text in the code for a bulk insert
            new SubCategoryRepository(_connectionString).Insert(obj);
         }
      }

      public void Update(SubCategoryModel obj)
      {
           var storedProc = "sp_update_sub_category";
           var updateObj = new
           {
                id = obj.Id,
                description = obj.Description
           };
           Update(storedProc, updateObj);
      }

      public void Delete(int id)
      {
           var storedProc = "sp_delete_sub_category";
           Delete(storedProc, id);
      }
   }
}
