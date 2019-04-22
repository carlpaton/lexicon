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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // GET: Category
        public IActionResult Index()
        {
            var dbModel = _categoryRepository.SelectList();
            var vwModel = _mapper.Map<List<CategoryViewModel>>(dbModel);

            return View(vwModel);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Description")] CategoryViewModel vwModel)
        {
            if (ModelState.IsValid)
            {
                var dbModel = _mapper.Map<CategoryModel>(vwModel);
                _categoryRepository.Insert(dbModel);

                ViewData["message"] = "Category added successfully.";
                ModelState.SetModelValue("Description", new ValueProviderResult(""));

                return View(new CategoryViewModel());
            }
            return View(vwModel);
        }

        // GET: Category/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
              return NotFound();

            var dbModel = _categoryRepository.Select(id ?? 0);
            if (dbModel.Id == 0)
                return NotFound();

            var vwModel = _mapper.Map<CategoryViewModel>(dbModel);
            return View(vwModel);
        }

        // POST: LexiconEntry/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Description")] CategoryViewModel vwModel)
        {
            if (id != vwModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var dbModel = _mapper.Map<CategoryModel>(vwModel);
                    _categoryRepository.Update(dbModel);

                    ViewData["message"] = "Category updated successfully.";
                }
                catch (Exception ex)
                {
                    // TODO ~ log something?

                    return NotFound();
                }
            }

            return View(vwModel);
        }

        /// <summary>
        /// TODO ~ GET for `Delete` is pretty shit 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            
            TempData["message"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}