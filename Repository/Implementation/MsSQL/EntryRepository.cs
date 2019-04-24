using System.Collections.Generic;
using Repository.Implementation;
using Repository.Interface;
using Repository.Schema;

namespace Repository.MsSQL
{
   public class EntryRepository : MsSQLContext, IEntryRepository
   {
       private readonly string _connectionString;
        public EntryRepository(string connectionString) : base(connectionString)
        {
            // Shim for BulkInsert
            _connectionString = connectionString;
        }

        public EntryModel Select(int id)
        {
           var storedProc = "sp_select_entry";
           return Select<EntryModel>(storedProc, new { id });
        }

      public List<EntryModel> SelectList()
      {
           var storedProc = "sp_selectlist_entry";
           return SelectList<EntryModel>(storedProc);
      }

      public int Insert(EntryModel obj)
      {
           var storedProc = "sp_insert_entry";
           var insertObj = new
           {
                category_id = obj.CategoryId,
                sub_category_id = obj.SubCategoryId,
                lexicon_function = obj.LexiconFunction,
                recommendation = obj.Recommendation,
                notes = obj.Notes
           };
           return Insert(storedProc, insertObj);
      }

      public void InsertBulk(List<EntryModel> listPoco)
      {
         foreach (var obj in listPoco)
         {
            // sweet hack, although a new connection per insert will probably be used -_- perhaps it will pool? meh :D
            // probably better to just have the sql command text in the code for a bulk insert
            new EntryRepository(_connectionString).Insert(obj);
         }
      }

      public void Update(EntryModel obj)
      {
           var storedProc = "sp_update_entry";
           var updateObj = new
           {
                id = obj.Id,
                category_id = obj.CategoryId,
                sub_category_id = obj.SubCategoryId,
                lexicon_function = obj.LexiconFunction,
                recommendation = obj.Recommendation,
                notes = obj.Notes
           };
           Update(storedProc, updateObj);
      }

      public void Delete(int id)
      {
           var storedProc = "sp_delete_entry";
           Delete(storedProc, id);
      }
   }
}
