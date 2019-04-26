using OfficeOpenXml;
using Repository.Interface;
using Repository.MsSQL;
using Repository.Schema;
using System;
using System.IO;
using System.Linq;

namespace Import
{
    class Program
    {
        static readonly AppSettings _appSettings = new AppSettings();
        static readonly ICategoryRepository _categoryRepository = new CategoryRepository(_appSettings.Conn);
        static readonly ISubCategoryRepository _subCategoryRepository = new SubCategoryRepository(_appSettings.Conn);
        static readonly IPlatformRepository _platformRepository = new PlatformRepository(_appSettings.Conn);
        static readonly IEntryRepository _entryRepository = new EntryRepository(_appSettings.Conn);
        static readonly IEntryPlatformRepository _entryPlatformRepository = new EntryPlatformRepository(_appSettings.Conn);

        static void Main(string[] args)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(_appSettings.ImportFile))
                {
                    pck.Load(stream);
                }
                
                var ws = pck.Workbook.Worksheets.First(); //first workbook
                int totalCols = ws.Dimension.End.Column;
                var currentCategoryId = 0;
                var currentSubCategoryId = 0;
                var dbModel = new EntryModel();

                for (int line = _appSettings.Lowerbound; line <= _appSettings.Upperbound + 1; line++)
                { 
                    for (int column = 1; column <= totalCols; column++)
                    {
                        // Ah perfect, a little hack :D
                        if (_appSettings.PlatformLine.Equals(line) && column < _appSettings.PlatformStartColumn)
                            column = _appSettings.PlatformStartColumn;

                        var value = "";
                        if (ws.Cells[line, column].Value != null)
                            value = ws.Cells[line, column].Value.ToString();
                        
                        if (string.IsNullOrEmpty(value))
                            continue;
                        
                        // Crap in the file to ignore, maybe put in config
                        if (value.Equals("-"))
                            continue;                 

                        // Uncle Bob is going to smack me with the OCP stick :D
                        // calling `shot` in order if fine I guess as the format of `import.xlsx` file is known :D
                        if (IsPlatform(line, column, value))
                            continue;

                        if (IsCategory(line, column, value, ref currentCategoryId))
                            break;

                        if (IsSubCategory(line, column, value, ref currentSubCategoryId))
                            break;

                        // ah duck, it gets worse :D
                        switch(column)
                        { 
                            case 1:
                                dbModel.LexiconFunction = value;
                                break;
                            case 2:
                                dbModel.Recommendation = value;
                                break;
                            case 3:
                                dbModel.Notes = value;
                                break;

                                // could probably read this from the config however then we should really also do this for `1,2,3` above
                            case int n when (n >= 4 & n <= 10):
                                dbModel.CategoryId = currentCategoryId;
                                dbModel.SubCategoryId = currentSubCategoryId;

                                Process(column, value, dbModel, ws);
                                break;
                            default:

                                // Relying on the fact that there are more columns in the worksheet than 10 is not the best. Meh.
                                dbModel = new EntryModel();
                                break;
                        }
                    }

                    Console.WriteLine(line);
                }
            }

            Console.Write("** END ***");
            Console.Read();
        }

        private static void Process(int column, string value, EntryModel dbModel, ExcelWorksheet ws)
        {
            var entryId = 0;
            var entry = _entryRepository
                .SelectList()
                .Where(x => x.CategoryId.Equals(dbModel.CategoryId))
                .Where(x => x.SubCategoryId.Equals(dbModel.SubCategoryId))
                .Where(x => x.LexiconFunction.Equals(dbModel.LexiconFunction))
                .Where(x => x.Recommendation.Equals(dbModel.Recommendation))
                .Where(x => x.Notes.Equals(dbModel.Notes))
                .FirstOrDefault();

            if (entry != null)
                entryId = entry.Id;

            if (entryId.Equals(0))
                entryId = _entryRepository.Insert(dbModel);

            var platFormName = ws.Cells[_appSettings.PlatformLine, column].Value.ToString();
            var playFormId = _platformRepository
                    .SelectList()
                    .Where(x => x.Description.Equals(platFormName))
                    .FirstOrDefault()
                    .Id; 

            var entryPlatform = new EntryPlatformModel() 
            { 
                Description = value, 
                EntryId = entryId, 
                PlatformId = playFormId 
            };
            _entryPlatformRepository.Insert(entryPlatform);
        }

        internal static bool IsPlatform(int line, int column, string value)
        {
            var _value = value.Trim().ToLower();
            if (_appSettings.PlatformLine.Equals(line))
            {
                var dbModel = _platformRepository
                    .SelectList()
                    .Where(x => x.Description.Trim().ToLower().Equals(_value));
                
                if (dbModel.Count().Equals(0))
                    _platformRepository.Insert(new PlatformModel() { Description = value });

                return true;
            }

            return false;
        }

        internal static bool IsCategory(int line, int column, string value, ref int currentCategoryId)
        {
            var _value = value.Trim().ToLower();
            if (_appSettings.Category.Contains(line))
            {
                var dbModel = _categoryRepository
                    .SelectList()
                    .Where(x => x.Description.Trim().ToLower().Equals(_value));
                
                if (dbModel.Count().Equals(0))
                    currentCategoryId = _categoryRepository.Insert(new CategoryModel() { Description = value });
                else
                    currentCategoryId = _categoryRepository
                        .SelectList()
                        .Where(x => x.Description.Trim().ToLower().Equals(_value))
                        .FirstOrDefault().Id;

                return true;
            }

            return false;
        }

        internal static bool IsSubCategory(int line, int column, string value, ref int currentSubCategoryId)
        {
            var _value = value
                .Trim()
                .ToLower()
                .Replace("[", "")
                .Replace("]", "");

            if (_appSettings.SubCategory.Contains(line))
            {
                var dbModel = _subCategoryRepository
                    .SelectList()
                    .Where(x => x.Description.Trim().ToLower().Equals(_value));
                
                if (dbModel.Count().Equals(0))
                    currentSubCategoryId = _subCategoryRepository.Insert(new SubCategoryModel() { Description = value.Replace("[", "").Replace("]", "") });
                else
                    currentSubCategoryId = _subCategoryRepository
                        .SelectList()
                        .Where(x => x.Description.Trim().ToLower().Equals(_value))
                        .FirstOrDefault().Id;

                return true;
            }

            return false;
        }
    }
}
