using System.Collections.Generic;
using Repository.Implementation;
using Repository.Interface;
using Repository.Schema;

namespace Repository.MsSQL
{
   public class CategoryRepository : MsSQLContext, ICategoryRepository
   {
       private readonly string _connectionString;
        public CategoryRepository(string connectionString) : base(connectionString)
        {
            // Shim for BulkInsert
            _connectionString = connectionString;
        }

        public CategoryModel Select(int id)
        {
           var storedProc = "sp_select_category";
           return Select<CategoryModel>(storedProc, new { id });
        }

      public List<CategoryModel> SelectList()
      {
           var storedProc = "sp_selectlist_category";
           return SelectList<CategoryModel>(storedProc);
      }

      public int Insert(CategoryModel obj)
      {
           var storedProc = "sp_insert_category";
           var insertObj = new
           {
                description = obj.Description
           };
           return Insert(storedProc, insertObj);
      }

      public void InsertBulk(List<CategoryModel> listPoco)
      {
         foreach (var obj in listPoco)
         {
            // sweet hack, although a new connection per insert will probably be used -_- perhaps it will pool? meh :D
            // probably better to just have the sql command text in the code for a bulk insert
            new CategoryRepository(_connectionString).Insert(obj);
         }
      }

      public void Update(CategoryModel obj)
      {
           var storedProc = "sp_update_category";
           var updateObj = new
           {
                id = obj.Id,
                description = obj.Description
           };
           Update(storedProc, updateObj);
      }

      public void Delete(int id)
      {
           var storedProc = "sp_delete_category";
           Delete(storedProc, id);
      }
   }
}
