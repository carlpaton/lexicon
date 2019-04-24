using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Schema;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : Controller
    {
        private readonly IEntryRepository EntryRepository;

        public EntryController(IEntryRepository entryRepository)
        {
            EntryRepository = entryRepository;
        }

        // GET api/entry
        [HttpGet]
        public JsonResult Get()
        {
            var dbModel = EntryRepository.SelectList();
            return new JsonResult(dbModel);
        }

        // GET api/entry/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var dbModel = EntryRepository.Select(id);
            return new JsonResult(dbModel);
        }

        // POST api/entry
        [HttpPost]
        public JsonResult Post([FromBody] EntryModel value)
        {
            return new JsonResult(EntryRepository.Insert(value));
        }

        // PUT api/entry/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EntryModel value)
        {
            value.Id = id;
            EntryRepository.Update(value);
        }

        // DELETE api/entry/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            EntryRepository.Delete(id);
        }
    }
}
