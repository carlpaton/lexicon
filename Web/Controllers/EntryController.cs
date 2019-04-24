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
    public class EntryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEntryRepository _entryRepository;
        private readonly IViewDataSelectList _viewDataSelectList;

        public EntryController(IMapper mapper, IViewDataSelectList viewDataSelectList, IEntryRepository entryRepository)
        {
            _mapper = mapper;
            _entryRepository = entryRepository;
            _viewDataSelectList = viewDataSelectList;
        }

        // GET: LexiconEntry
        public IActionResult Index()
        {
            var dbModel = _entryRepository.SelectList();
            var vwModel = _mapper.Map<List<EntryViewModel>>(dbModel);

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
        public IActionResult Create([Bind("CategoryId,SubCategoryId,LexiconFunction,Recommendation,Notes")] EntryViewModel vwModel)
        {
            if (ModelState.IsValid)
            {
                var dbModel = _mapper.Map<EntryModel>(vwModel);
                _entryRepository.Insert(dbModel);

                ViewData["message"] = "Entry added successfully.";
                ModelState.SetModelValue("CategoryId", new ValueProviderResult(""));
                ModelState.SetModelValue("SubCategoryId", new ValueProviderResult(""));
                ModelState.SetModelValue("LexiconFunction", new ValueProviderResult(""));
                ModelState.SetModelValue("Recommendation", new ValueProviderResult(""));
                ModelState.SetModelValue("Notes", new ValueProviderResult(""));

                SetSelectList();
                return View(new EntryViewModel());
            }
            return View(vwModel);
        }

        // GET: LexiconEntry/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
              return NotFound();

            var dbModel = _entryRepository.Select(id ?? 0);
            if (dbModel.Id == 0)
                return NotFound();

            var vwModel = _mapper.Map<EntryViewModel>(dbModel);
            SetSelectList();
            return View(vwModel);
        }

        // POST: LexiconEntry/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CategoryId,SubCategoryId,LexiconFunction,Recommendation,Notes")] EntryViewModel vwModel)
        {
            if (id != vwModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var dbModel = _mapper.Map<EntryModel>(vwModel);
                    _entryRepository.Update(dbModel);

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

        // POST: LexiconEntry/Delete/5
        [HttpPost, ActionName("Delete")] 
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _entryRepository.Delete(id);
            TempData["message"] = "Entry deleted successfully.";

            return RedirectToAction("Index");
        }

        public void SetSelectList()
        {
            ViewData["Category_SelectList"] = _viewDataSelectList.CategorySelectList(_mapper);
            ViewData["SubCategory_SelectList"] = _viewDataSelectList.SubCategorySelectList(_mapper);
        }
    }
}
