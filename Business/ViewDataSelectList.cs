using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Business
{
    public interface IViewDataSelectList
    {
        List<SelectListItem> CategorySelectList(IMapper mapper);
        List<SelectListItem> SubCategorySelectList(IMapper mapper);
        List<SelectListItem> PlatformSelectList(IMapper mapper);
    }

    public class ViewDataSelectList : IViewDataSelectList
    {
        private readonly IEntryBusiness _entryBusiness;
        // public delegate void HoeDelegate();

        public ViewDataSelectList (IEntryBusiness entryBusiness)
        {
            _entryBusiness = entryBusiness;
        }

        /* TODO
         * These methods are simliar change to have one method `SelectList`
         * pass in an `IEnumerable` list
         * `yield return` the response
         */

        public List<SelectListItem> CategorySelectList(IMapper mapper)
        {
            var bu = _entryBusiness.GetModel(mapper);
            var selectList = new List<SelectListItem>();
            foreach (var item in bu.Category)
            {
                selectList.Add(new SelectListItem()
                {
                    Text = item.Description,
                    Value = item.Id.ToString()
                });
            }
            return selectList;
        }

        public List<SelectListItem> SubCategorySelectList(IMapper mapper)
        {
            var bu = _entryBusiness.GetModel(mapper);
            var selectList = new List<SelectListItem>();
            foreach (var item in bu.SubCategory)
            {
                selectList.Add(new SelectListItem()
                {
                    Text = item.Description,
                    Value = item.Id.ToString()
                });
            }
            return selectList;
        }

        public List<SelectListItem> PlatformSelectList(IMapper mapper)
        {
            var bu = _entryBusiness.GetModel(mapper);
            var selectList = new List<SelectListItem>();
            foreach (var item in bu.Platform)
            {
                selectList.Add(new SelectListItem()
                {
                    Text = item.Description,
                    Value = item.Id.ToString()
                });
            }
            return selectList;
        }
    }
}
