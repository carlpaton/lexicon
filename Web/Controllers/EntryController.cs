using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
            var vwModel = TrucateTextFields(_mapper.Map<List<EntryViewModel>>(dbModel));
            
            SetSelectList();
            return View(vwModel);
        }

        // GET: Filter/?searchFilter=x
        public IActionResult Filter(string searchFilter, int categoryFilterId, int subCategoryFilterId)
        {
            var dbModel = _entryRepository.SelectList();

            #region TODO ~ move into a service
            if (!string.IsNullOrEmpty(searchFilter))
            {
                // this is an `OR` operator as `searchFilter` is one field
                var filterLexiconFunction = dbModel
                    .Where(x => x.LexiconFunction.Contains(searchFilter))
                    .ToList();

                var recommendation = dbModel
                    .Where(x => x.Recommendation.Contains(searchFilter))
                    .ToList();

                var notes = dbModel
                    .Where(x => x.Notes.Contains(searchFilter))
                    .ToList();

                dbModel = new List<EntryModel>();
                dbModel.AddRange(filterLexiconFunction);
                dbModel.AddRange(recommendation);
                dbModel.AddRange(notes);
                dbModel.OrderBy(x => x.Id);
            }

            if (categoryFilterId > 0)
            {
                // this is an `AND` operator
                dbModel = dbModel.Where(x => x.CategoryId.Equals((categoryFilterId))).ToList();
            }   
            
            if (subCategoryFilterId > 0)
            {
                // this is an `AND` operator
                dbModel = dbModel.Where(x => x.SubCategoryId.Equals((subCategoryFilterId))).ToList();
            } 
            #endregion

            var vwModel = _mapper.Map<List<EntryViewModel>>(dbModel);
            SetSelectList();
            return PartialView("_Table", vwModel);
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

        /// <summary>
        /// TODO ~ DI much? :D
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private object TrucateTextFields(List<EntryViewModel> list)
        {
            var maxLen = 40;
            var r = new List<EntryViewModel>();

            //TODO ~ there must be a way to do this with linq
            //var notes = list.Select(w => w.Notes.Substring(0, 15)).ToList();

            foreach (var item in list)
            {
                r.Add(new EntryViewModel()
                {
                    Id = item.Id,
                    CategoryId = item.CategoryId,
                    SubCategoryId = item.SubCategoryId,
                    Notes = item.Notes.Length > maxLen ? item.Notes.Substring(0, maxLen) + "..." : item.Notes,
                    Recommendation = item.Recommendation.Length > maxLen ? item.Recommendation.Substring(0, maxLen) + "..." : item.Recommendation,
                    LexiconFunction = item.LexiconFunction.Length > maxLen ? item.LexiconFunction.Substring(0, maxLen) + "..." : item.LexiconFunction
                });
            }

            return r;
        }
    }
}
