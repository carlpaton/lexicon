using AutoMapper;
using Business;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Web.Models;

namespace Web.Controllers
{
    public class EntryPlatformController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEntryRepository _entryRepository;
        private readonly IEntryPlatformRepository _entryPlatformRepository;
        private readonly IViewDataSelectList _viewDataSelectList;

        public EntryPlatformController(IMapper mapper, IViewDataSelectList viewDataSelectList, IEntryRepository entryRepository, IEntryPlatformRepository entryPlatformRepository)
        {
            _mapper = mapper;
            _entryRepository = entryRepository;
            _viewDataSelectList = viewDataSelectList;        
            _entryPlatformRepository = entryPlatformRepository;
        }

        public IActionResult Index(int id)
        {
            if (id == 0)
              return NotFound();

            var entry = _entryRepository.Select(id); 
            if (entry.Id == 0)
                return NotFound();

            var entryPlatform = _entryPlatformRepository.SelectListByEntryId(id); 

            // TODO
            // move this to mapping `Business` 
            var vwModel = new EntryPlatformViewModel();
            vwModel.Entry.Id = entry.Id;
            vwModel.Entry.CategoryId = entry.CategoryId;
            vwModel.Entry.LexiconFunction = entry.LexiconFunction;
            vwModel.Entry.Notes = entry.Notes;
            vwModel.Entry.Recommendation = entry.Recommendation;
            vwModel.Entry.SubCategoryId = entry.SubCategoryId;

            foreach (var item in entryPlatform)
            {
                vwModel.EntryPlatformList.Add(new EntryPlatform()
                { 
                    Description = item.Description,
                    EntryId = item.EntryId,
                    Id = item.Id,
                    PlatformId = item.PlatformId
                });
            }

            ViewData["Category_SelectList"] = _viewDataSelectList.CategorySelectList(_mapper);
            ViewData["SubCategory_SelectList"] = _viewDataSelectList.SubCategorySelectList(_mapper);
            ViewData["Platform_SelectList"] = _viewDataSelectList.PlatformSelectList(_mapper);
            return View(vwModel);
        }
    }
}