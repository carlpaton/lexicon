using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Schema;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository CategoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        // GET api/category
        [HttpGet]
        public JsonResult Get()
        {
            var dbModel = CategoryRepository.SelectList();
            return new JsonResult(dbModel);
        }

        // GET api/category/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var dbModel = CategoryRepository.Select(id);
            return new JsonResult(dbModel);
        }

        // POST api/category
        [HttpPost]
        public JsonResult Post([FromBody] CategoryModel value)
        {
            return new JsonResult(CategoryRepository.Insert(value));
        }

        // PUT api/category/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CategoryModel value)
        {
            value.Id = id;
            CategoryRepository.Update(value);
        }

        // DELETE api/category/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            CategoryRepository.Delete(id);
        }
    }
}
