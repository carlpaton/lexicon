using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Repository.Interface;
using Repository.Schema;
using System;
using System.Collections.Generic;
using Web.Models;

namespace Web.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMapper _mapper;

        public SubCategoryController(ISubCategoryRepository subCategoryRepository, IMapper mapper)
        {
            _subCategoryRepository = subCategoryRepository;
            _mapper = mapper;
        }

        // GET: SubCategory
        public IActionResult Index()
        {
            var dbModel = _subCategoryRepository.SelectList();
            var vwModel = _mapper.Map<List<SubCategoryViewModel>>(dbModel);

            return View(vwModel);
        }

        // GET: SubCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Description")] SubCategoryViewModel vwModel)
        {
            if (ModelState.IsValid)
            {
                var dbModel = _mapper.Map<SubCategoryModel>(vwModel);
                _subCategoryRepository.Insert(dbModel);

                ViewData["message"] = "Sub category added successfully.";
                ModelState.SetModelValue("Description", new ValueProviderResult(""));

                return View(new SubCategoryViewModel());
            }
            return View(vwModel);
        }

        // GET: SubCategory/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
              return NotFound();

            var dbModel = _subCategoryRepository.Select(id ?? 0);
            if (dbModel.Id == 0)
                return NotFound();

            var vwModel = _mapper.Map<SubCategoryViewModel>(dbModel);
            return View(vwModel);
        }

        // POST: SubCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Description")] SubCategoryViewModel vwModel)
        {
            if (id != vwModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var dbModel = _mapper.Map<SubCategoryModel>(vwModel);
                    _subCategoryRepository.Update(dbModel);

                    ViewData["message"] = "Sub category updated successfully.";
                }
                catch (Exception ex)
                {
                    // TODO ~ log something?

                    return NotFound();
                }
            }

            return View(vwModel);
        }

        // POST: SubCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _subCategoryRepository.Delete(id);
            
            TempData["message"] = "Sub category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}