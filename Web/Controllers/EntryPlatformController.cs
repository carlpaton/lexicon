using AutoMapper;
using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Repository.Interface;
using Repository.Schema;
using System;
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

        // GET: EntryPlatform/Index/5
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
                vwModel.EntryPlatformList.Add(new EntryPlatformListViewModel()
                { 
                    Description = item.Description,
                    EntryId = item.EntryId,
                    Id = item.Id,
                    PlatformId = item.PlatformId
                });
            }

            SetSelectList();
            return View(vwModel);
        }

        // GET: EntryPlatform/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
              return NotFound();

            var dbModel = _entryPlatformRepository.Select(id ?? 0);
            if (dbModel.Id == 0)
                return NotFound();

            var vwModel = _mapper.Map<EntryPlatformListViewModel>(dbModel);
            SetSelectList();
            return View(vwModel);
        }

        // POST: EntryPlatform/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,EntryId,PlatformId,Description")] EntryPlatformListViewModel vwModel)
        {
            if (id != vwModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var dbModel = _mapper.Map<EntryPlatformModel>(vwModel);
                    _entryPlatformRepository.Update(dbModel);

                    ViewData["message"] = "Entry updated successfully.";
                }
                catch (Exception ex)
                {
                    // TODO ~ log something?
                    return NotFound();
                }
            }

            SetSelectList();
            return View(vwModel);
        }

        // GET: EntryPlatform/Create/5
        public IActionResult Create(int id)
        {
            if (id == 0)
                return NotFound();

            var vwModel = new EntryPlatformListViewModel()
            {
                EntryId = id                
            };
            SetSelectList();
            return View(vwModel);
        }

        // POST: EntryPlatform/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EntryId,PlatformId,Description")] EntryPlatformListViewModel vwModel)
        {
            if (ModelState.IsValid)
            {
                var dbModel = _mapper.Map<EntryPlatformModel>(vwModel);
                _entryPlatformRepository.Insert(dbModel);

                ViewData["message"] = "PLatform entry added successfully.";
                ModelState.SetModelValue("Description", new ValueProviderResult(""));
                ModelState.SetModelValue("PlatformId", new ValueProviderResult(""));

                var vwModel2 = new EntryPlatformListViewModel()
                {
                    EntryId = vwModel.EntryId                
                };
                SetSelectList();
                return View(vwModel2);
            }
            return View(vwModel);
        }

        // POST: EntryPlatform/Delete/5?entryId=6
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, int entryId)
        {
            _entryPlatformRepository.Delete(id);
            TempData["message"] = "Platform entry deleted successfully.";
            
            return RedirectToAction(
                "Index",
                "EntryPlatform",
                new { id = entryId });
        }

        public void SetSelectList()
        {
            ViewData["Category_SelectList"] = _viewDataSelectList.CategorySelectList(_mapper);
            ViewData["SubCategory_SelectList"] = _viewDataSelectList.SubCategorySelectList(_mapper);
            ViewData["Platform_SelectList"] = _viewDataSelectList.PlatformSelectList(_mapper);
        }
    }
}