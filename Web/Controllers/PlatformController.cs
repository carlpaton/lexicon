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
    public class PlatformController : Controller
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;

        public PlatformController(IPlatformRepository platformRepository, IMapper mapper)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
        }

        // GET: Platform
        public IActionResult Index()
        {
            var dbModel = _platformRepository.SelectList();
            var vwModel = _mapper.Map<List<PlatformViewModel>>(dbModel);

            return View(vwModel);
        }

        // GET: Platform/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Platform/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Description")] PlatformViewModel vwModel)
        {
            if (ModelState.IsValid)
            {
                var dbModel = _mapper.Map<PlatformModel>(vwModel);
                _platformRepository.Insert(dbModel);

                ViewData["message"] = "Platform added successfully.";
                ModelState.SetModelValue("Description", new ValueProviderResult(""));

                return View(new PlatformViewModel());
            }
            return View(vwModel);
        }

        // GET: Platform/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
              return NotFound();

            var dbModel = _platformRepository.Select(id ?? 0);
            if (dbModel.Id == 0)
                return NotFound();

            var vwModel = _mapper.Map<PlatformViewModel>(dbModel);
            return View(vwModel);
        }

        // POST: Platform/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Description")] PlatformViewModel vwModel)
        {
            if (id != vwModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var dbModel = _mapper.Map<PlatformModel>(vwModel);
                    _platformRepository.Update(dbModel);

                    ViewData["message"] = "Platform updated successfully.";
                }
                catch (Exception ex)
                {
                    // TODO ~ log something?

                    return NotFound();
                }
            }

            return View(vwModel);
        }

        // POST: Platform/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _platformRepository.Delete(id);
            TempData["message"] = "Platform deleted successfully.";
            
            return RedirectToAction("Index");
        }
    }
}