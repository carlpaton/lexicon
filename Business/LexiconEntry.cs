using AutoMapper;
using Business.Models;
using Repository.Interface;
using System.Collections.Generic;

namespace Business
{
    public interface ILexiconEntryBusiness
    {
        LexiconEntryBusinessModel GetModel(IMapper _mapper);
    }

    public class LexiconEntryBusiness : ILexiconEntryBusiness
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPlatformRepository _platformRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ILexiconEntryTypeRepository _lexiconEntryTypeRepository;

        public LexiconEntryBusiness(ICategoryRepository categoryRepository, IPlatformRepository platformRepository, ISubCategoryRepository subCategoryRepository, ILexiconEntryTypeRepository lexiconEntryTypeRepository)
        {
            _categoryRepository = categoryRepository;
            _platformRepository = platformRepository;
            _subCategoryRepository = subCategoryRepository;
            _lexiconEntryTypeRepository = lexiconEntryTypeRepository;
        }

        public LexiconEntryBusinessModel GetModel(IMapper _mapper)
        {
            var category = _categoryRepository.SelectList();
            var platform = _platformRepository.SelectList();
            var subCategory = _subCategoryRepository.SelectList();
            var lexiconEntry = _lexiconEntryTypeRepository.SelectList();

            var lexiconEntryBusiness = new LexiconEntryBusinessModel();
            lexiconEntryBusiness.Category.AddRange(_mapper.Map<List<CategoryBusinessModel>>(category));
            lexiconEntryBusiness.Platform.AddRange(_mapper.Map<List<PlatformBusinessModel>>(platform));
            lexiconEntryBusiness.SubCategory.AddRange(_mapper.Map<List<SubCategoryBusinessModel>>(subCategory));
            lexiconEntryBusiness.LexiconEntryType.AddRange(_mapper.Map<List<LexiconEntryTypeBusinessModel>>(lexiconEntry));

            return lexiconEntryBusiness;
        }
    }
}
