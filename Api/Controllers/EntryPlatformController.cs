using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Schema;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryPlatformController : Controller
    {
        private readonly IEntryPlatformRepository EntryPlatformRepository;

        public EntryPlatformController(IEntryPlatformRepository entryPlatformRepository)
        {
            EntryPlatformRepository = entryPlatformRepository;
        }

        // GET api/entryplatform
        [HttpGet]
        public JsonResult Get()
        {
            var dbModel = EntryPlatformRepository.SelectList();
            return new JsonResult(dbModel);
        }

        // GET api/entryplatform/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var dbModel = EntryPlatformRepository.Select(id);
            return new JsonResult(dbModel);
        }

        // POST api/entryplatform
        [HttpPost]
        public JsonResult Post([FromBody] EntryPlatformModel value)
        {
            return new JsonResult(EntryPlatformRepository.Insert(value));
        }

        // PUT api/entryplatform/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EntryPlatformModel value)
        {
            value.Id = id;
            EntryPlatformRepository.Update(value);
        }

        // DELETE api/entryplatform/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            EntryPlatformRepository.Delete(id);
        }
    }
}
