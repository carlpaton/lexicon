using System.Collections.Generic;
using Repository.Implementation;
using Repository.Interface;
using Repository.Schema;

namespace Repository.MsSQL
{
   public class LexiconEntryTypeRepository : MsSQLContext, ILexiconEntryTypeRepository
   {
       private readonly string _connectionString;
        public LexiconEntryTypeRepository(string connectionString) : base(connectionString)
        {
            // Shim for BulkInsert
            _connectionString = connectionString;
        }

        public LexiconEntryTypeModel Select(int id)
        {
           var storedProc = "sp_select_lexicon_entry_type";
           return Select<LexiconEntryTypeModel>(storedProc, new { id });
        }

      public List<LexiconEntryTypeModel> SelectList()
      {
           var storedProc = "sp_selectlist_lexicon_entry_type";
           return SelectList<LexiconEntryTypeModel>(storedProc);
      }

      public int Insert(LexiconEntryTypeModel obj)
      {
           var storedProc = "sp_insert_lexicon_entry_type";
           var insertObj = new
           {
                description = obj.Description
           };
           return Insert(storedProc, insertObj);
      }

      public void InsertBulk(List<LexiconEntryTypeModel> listPoco)
      {
         foreach (var obj in listPoco)
         {
            // sweet hack, although a new connection per insert will probably be used -_- perhaps it will pool? meh :D
            // probably better to just have the sql command text in the code for a bulk insert
            new LexiconEntryTypeRepository(_connectionString).Insert(obj);
         }
      }

      public void Update(LexiconEntryTypeModel obj)
      {
           var storedProc = "sp_update_lexicon_entry_type";
           var updateObj = new
           {
                id = obj.Id,
                description = obj.Description
           };
           Update(storedProc, updateObj);
      }

      public void Delete(int id)
      {
           var storedProc = "sp_delete_lexicon_entry_type";
           Delete(storedProc, id);
      }
   }
}
