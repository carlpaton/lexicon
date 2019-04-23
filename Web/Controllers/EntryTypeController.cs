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
    public class EntryTypeController : Controller
    {
        private readonly ILexiconEntryTypeRepository _entryTypeRepository;
        private readonly IMapper _mapper;

        public EntryTypeController(ILexiconEntryTypeRepository entryTypeRepository, IMapper mapper)
        {
            _entryTypeRepository = entryTypeRepository;
            _mapper = mapper;
        }

        // GET: EntryType
        public IActionResult Index()
        {
            var dbModel = _entryTypeRepository.SelectList();
            var vwModel = _mapper.Map<List<LexiconEntryTypeViewModel>>(dbModel);

            return View(vwModel);
        }

        // GET: EntryType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EntryType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Description")] LexiconEntryTypeViewModel vwModel)
        {
            if (ModelState.IsValid)
            {
                var dbModel = _mapper.Map<LexiconEntryTypeModel>(vwModel);
                _entryTypeRepository.Insert(dbModel);

                ViewData["message"] = "Entry type added successfully.";
                ModelState.SetModelValue("Description", new ValueProviderResult(""));

                return View(new LexiconEntryTypeViewModel());
            }
            return View(vwModel);
        }

        // GET: EntryType/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
              return NotFound();

            var dbModel = _entryTypeRepository.Select(id ?? 0);
            if (dbModel.Id == 0)
                return NotFound();

            var vwModel = _mapper.Map<LexiconEntryTypeViewModel>(dbModel);
            return View(vwModel);
        }

        // POST: EntryType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Description")] LexiconEntryTypeViewModel vwModel)
        {
            if (id != vwModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var dbModel = _mapper.Map<LexiconEntryTypeModel>(vwModel);
                    _entryTypeRepository.Update(dbModel);

                    ViewData["message"] = "Entry type updated successfully.";
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
            _entryTypeRepository.Delete(id);
            
            TempData["message"] = "Entry type deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}