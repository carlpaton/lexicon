using System.Collections.Generic;
using Repository.Implementation;
using Repository.Interface;
using Repository.Schema;

namespace Repository.MsSQL
{
   public class LexiconEntryRepository : MsSQLContext, ILexiconEntryRepository
   {
       private readonly string _connectionString;
        public LexiconEntryRepository(string connectionString) : base(connectionString)
        {
            // Shim for BulkInsert
            _connectionString = connectionString;
        }

        public LexiconEntryModel Select(int id)
        {
           var storedProc = "sp_select_lexicon_entry";
           return Select<LexiconEntryModel>(storedProc, new { id });
        }

      public List<LexiconEntryModel> SelectList()
      {
           var storedProc = "sp_selectlist_lexicon_entry";
           return SelectList<LexiconEntryModel>(storedProc);
      }

      public int Insert(LexiconEntryModel obj)
      {
           var storedProc = "sp_insert_lexicon_entry";
           var insertObj = new
           {
                category_id = obj.CategoryId,
                platform_id = obj.PlatformId,
                sub_category_id = obj.SubCategoryId,
                lexicon_entry_type_id = obj.LexiconEntryTypeId,
                description = obj.Description
           };
           return Insert(storedProc, insertObj);
      }

      public void InsertBulk(List<LexiconEntryModel> listPoco)
      {
         foreach (var obj in listPoco)
         {
            // sweet hack, although a new connection per insert will probably be used -_- perhaps it will pool? meh :D
            // probably better to just have the sql command text in the code for a bulk insert
            new LexiconEntryRepository(_connectionString).Insert(obj);
         }
      }

      public void Update(LexiconEntryModel obj)
      {
           var storedProc = "sp_update_lexicon_entry";
           var updateObj = new
           {
                id = obj.Id,
                category_id = obj.CategoryId,
                platform_id = obj.PlatformId,
                sub_category_id = obj.SubCategoryId,
                lexicon_entry_type_id = obj.LexiconEntryTypeId,
                description = obj.Description
           };
           Update(storedProc, updateObj);
      }

      public void Delete(int id)
      {
           var storedProc = "sp_delete_lexicon_entry";
           Delete(storedProc, id);
      }
   }
}
