using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Schema;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LexiconEntryController : Controller
    {
        private readonly ILexiconEntryRepository LexiconEntryRepository;

        public LexiconEntryController(ILexiconEntryRepository lexiconEntryRepository)
        {
            LexiconEntryRepository = lexiconEntryRepository;
        }

        // GET api/lexiconentry
        [HttpGet]
        public JsonResult Get()
        {
            var dbModel = LexiconEntryRepository.SelectList();
            return new JsonResult(dbModel);
        }

        // GET api/lexiconentry/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var dbModel = LexiconEntryRepository.Select(id);
            return new JsonResult(dbModel);
        }

        // POST api/lexiconentry
        [HttpPost]
        public JsonResult Post([FromBody] LexiconEntryModel value)
        {
            return new JsonResult(LexiconEntryRepository.Insert(value));
        }

        // PUT api/lexiconentry/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] LexiconEntryModel value)
        {
            value.Id = id;
            LexiconEntryRepository.Update(value);
        }

        // DELETE api/lexiconentry/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            LexiconEntryRepository.Delete(id);
        }
    }
}
