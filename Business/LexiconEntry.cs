using AutoMapper;
using Business.Models;
using Repository.Interface;
using System.Collections.Generic;

namespace Business
{
    public interface IEntryBusiness
    {
        EntryBusinessModel GetModel(IMapper _mapper);
    }

    public class EntryBusiness : IEntryBusiness
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IPlatformRepository _platformRepository;

        public EntryBusiness(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IPlatformRepository platformRepository)
        {
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _platformRepository = platformRepository;
        }

        public EntryBusinessModel GetModel(IMapper _mapper)
        {
            var category = _categoryRepository.SelectList();
            var subCategory = _subCategoryRepository.SelectList();
            var platform = _platformRepository.SelectList();

            var entryBusiness = new EntryBusinessModel();
            entryBusiness.Category.AddRange(_mapper.Map<List<CategoryBusinessModel>>(category));
            entryBusiness.SubCategory.AddRange(_mapper.Map<List<SubCategoryBusinessModel>>(subCategory));
            entryBusiness.Platform.AddRange(_mapper.Map<List<PlatformBusinessModel>>(platform));

            return entryBusiness;
        }
    }
}
