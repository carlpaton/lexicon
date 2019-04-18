using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Business
{
    public interface IViewDataSelectList
    {
        List<SelectListItem> CategorySelectList(IMapper mapper);
        List<SelectListItem> PlatformSelectList(IMapper mapper);
        List<SelectListItem> SubCategorySelectList(IMapper mapper);
        List<SelectListItem> LexiconEntryTypeSelectList(IMapper mapper);
    }

    public class ViewDataSelectList : IViewDataSelectList
    {
        private readonly ILexiconEntryBusiness _lexiconEntryBusiness;
        public delegate void HoeDelegate();

        public ViewDataSelectList (ILexiconEntryBusiness lexiconEntryBusiness)
        {
            _lexiconEntryBusiness = lexiconEntryBusiness;
        }

        /* TODO
         * These methods are simliar change to have one method `SelectList`
         * pass in an `IEnumerable` list
         * `yield return` the response
         */

        public List<SelectListItem> CategorySelectList(IMapper mapper)
        {
            var lexiconEntryBusinessModel = _lexiconEntryBusiness.GetModel(mapper);
            var selectList = new List<SelectListItem>();
            foreach (var item in lexiconEntryBusinessModel.Category)
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
            var lexiconEntryBusinessModel = _lexiconEntryBusiness.GetModel(mapper);
            var selectList = new List<SelectListItem>();
            foreach (var item in lexiconEntryBusinessModel.Platform)
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
            var lexiconEntryBusinessModel = _lexiconEntryBusiness.GetModel(mapper);
            var selectList = new List<SelectListItem>();
            foreach (var item in lexiconEntryBusinessModel.SubCategory)
            {
                selectList.Add(new SelectListItem()
                {
                    Text = item.Description,
                    Value = item.Id.ToString()
                });
            }
            return selectList;
        }

        public List<SelectListItem> LexiconEntryTypeSelectList(IMapper mapper)
        {
            var lexiconEntryBusinessModel = _lexiconEntryBusiness.GetModel(mapper);
            var selectList = new List<SelectListItem>();
            foreach (var item in lexiconEntryBusinessModel.LexiconEntryType)
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
