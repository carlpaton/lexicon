using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Schema;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryRepository SubCategoryRepository;

        public SubCategoryController(ISubCategoryRepository subCategoryRepository)
        {
            SubCategoryRepository = subCategoryRepository;
        }

        // GET api/subcategory
        [HttpGet]
        public JsonResult Get()
        {
            var dbModel = SubCategoryRepository.SelectList();
            return new JsonResult(dbModel);
        }

        // GET api/subcategory/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var dbModel = SubCategoryRepository.Select(id);
            return new JsonResult(dbModel);
        }

        // POST api/subcategory
        [HttpPost]
        public JsonResult Post([FromBody] SubCategoryModel value)
        {
            return new JsonResult(SubCategoryRepository.Insert(value));
        }

        // PUT api/subcategory/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] SubCategoryModel value)
        {
            value.Id = id;
            SubCategoryRepository.Update(value);
        }

        // DELETE api/subcategory/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            SubCategoryRepository.Delete(id);
        }
    }
}
