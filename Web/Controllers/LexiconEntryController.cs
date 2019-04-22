using System;
using System.Collections.Generic;
using AutoMapper;
using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Repository.Interface;
using Repository.Schema;
using Web.Models;

namespace Web.Controllers
{
    public class LexiconEntryController : Controller
    {
        private readonly ILexiconEntryRepository _lexiconEntryRepository;
        private readonly IMapper _mapper;
        private readonly IViewDataSelectList _viewDataSelectList;

        public LexiconEntryController(ILexiconEntryRepository lexiconEntryRepository, IMapper mapper, IViewDataSelectList viewDataSelectList)
        {
            _lexiconEntryRepository = lexiconEntryRepository;
            _mapper = mapper;
            _viewDataSelectList = viewDataSelectList;
        }

        // GET: LexiconEntry
        public IActionResult Index()
        {
            var dbModel = _lexiconEntryRepository.SelectList();
            var vwModel = _mapper.Map<List<LexiconEntryViewModel>>(dbModel);

            SetSelectList();
            return View(vwModel);
        }

        // GET: LexiconEntry/Create
        public IActionResult Create()
        {
            SetSelectList();
            return View();
        }

        // POST: LexiconEntry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoryId,PlatformId,SubCategoryId,LexiconEntryTypeId,Description")] LexiconEntryViewModel vwModel)
        {
            if (ModelState.IsValid)
            {
                var dbModel = _mapper.Map<LexiconEntryModel>(vwModel);
                _lexiconEntryRepository.Insert(dbModel);

                ViewData["message"] = "Lexicon entry added successfully.";
                ModelState.SetModelValue("CategoryId", new ValueProviderResult(""));
                ModelState.SetModelValue("PlatformId", new ValueProviderResult(""));
                ModelState.SetModelValue("SubCategoryId", new ValueProviderResult(""));
                ModelState.SetModelValue("LexiconEntryTypeId", new ValueProviderResult(""));
                ModelState.SetModelValue("Description", new ValueProviderResult(""));

                SetSelectList();
                return View(new LexiconEntryViewModel());
            }
            return View(vwModel);
        }

        // GET: LexiconEntry/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
              return NotFound();

            var dbModel = _lexiconEntryRepository.Select(id ?? 0);
            if (dbModel.Id == 0)
                return NotFound();

            var vwModel = _mapper.Map<LexiconEntryViewModel>(dbModel);
            SetSelectList();
            return View(vwModel);
        }

        // POST: LexiconEntry/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CategoryId,PlatformId,SubCategoryId,LexiconEntryTypeId,Description")] LexiconEntryViewModel vwModel)
        {
            if (id != vwModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var dbModel = _mapper.Map<LexiconEntryModel>(vwModel);
                    _lexiconEntryRepository.Update(dbModel);

                    ViewData["message"] = "Lexicon entry updated successfully.";
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

        // TODO ~ we want to safe guard this and rather return `405` if some clown does a GET here
        // Currently `Index.html` has a link to this method in the `delete`, this is shit :D
        
        // POST: LexiconEntry/Delete/5
        //[HttpPost, ActionName("Delete")] 
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _lexiconEntryRepository.Delete(id);
            
            TempData["message"] = "Lexicon entry deleted successfully.";
            return RedirectToAction("Index");
        }

        public void SetSelectList()
        {
            ViewData["Category_SelectList"] = _viewDataSelectList.CategorySelectList(_mapper);
            ViewData["Platform_SelectList"] = _viewDataSelectList.PlatformSelectList(_mapper);
            ViewData["SubCategory_SelectList"] = _viewDataSelectList.SubCategorySelectList(_mapper);
            ViewData["LexiconEntryType_SelectList"] = _viewDataSelectList.LexiconEntryTypeSelectList(_mapper);
        }
    }
}
