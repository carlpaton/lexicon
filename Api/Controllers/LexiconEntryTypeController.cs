using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Schema;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LexiconEntryTypeController : Controller
    {
        private readonly ILexiconEntryTypeRepository LexiconEntryTypeRepository;

        public LexiconEntryTypeController(ILexiconEntryTypeRepository lexiconEntryTypeRepository)
        {
            LexiconEntryTypeRepository = lexiconEntryTypeRepository;
        }

        // GET api/lexiconentrytype
        [HttpGet]
        public JsonResult Get()
        {
            var dbModel = LexiconEntryTypeRepository.SelectList();
            return new JsonResult(dbModel);
        }

        // GET api/lexiconentrytype/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var dbModel = LexiconEntryTypeRepository.Select(id);
            return new JsonResult(dbModel);
        }

        // POST api/lexiconentrytype
        [HttpPost]
        public JsonResult Post([FromBody] LexiconEntryTypeModel value)
        {
            return new JsonResult(LexiconEntryTypeRepository.Insert(value));
        }

        // PUT api/lexiconentrytype/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] LexiconEntryTypeModel value)
        {
            value.Id = id;
            LexiconEntryTypeRepository.Update(value);
        }

        // DELETE api/lexiconentrytype/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            LexiconEntryTypeRepository.Delete(id);
        }
    }
}
