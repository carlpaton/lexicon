using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Schema;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : Controller
    {
        private readonly IPlatformRepository PlatformRepository;

        public PlatformController(IPlatformRepository platformRepository)
        {
            PlatformRepository = platformRepository;
        }

        // GET api/platform
        [HttpGet]
        public JsonResult Get()
        {
            var dbModel = PlatformRepository.SelectList();
            return new JsonResult(dbModel);
        }

        // GET api/platform/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var dbModel = PlatformRepository.Select(id);
            return new JsonResult(dbModel);
        }

        // POST api/platform
        [HttpPost]
        public JsonResult Post([FromBody] PlatformModel value)
        {
            return new JsonResult(PlatformRepository.Insert(value));
        }

        // PUT api/platform/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PlatformModel value)
        {
            value.Id = id;
            PlatformRepository.Update(value);
        }

        // DELETE api/platform/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PlatformRepository.Delete(id);
        }
    }
}
