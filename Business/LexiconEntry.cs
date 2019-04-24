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

        public EntryBusiness(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
        }

        public EntryBusinessModel GetModel(IMapper _mapper)
        {
            var category = _categoryRepository.SelectList();
            var subCategory = _subCategoryRepository.SelectList();

            var entryBusiness = new EntryBusinessModel();
            entryBusiness.Category.AddRange(_mapper.Map<List<CategoryBusinessModel>>(category));
            entryBusiness.SubCategory.AddRange(_mapper.Map<List<SubCategoryBusinessModel>>(subCategory));

            // TODO 
            // map 
            // map `EntryPlatform.PlatformModel`

            return entryBusiness;
        }
    }
}
