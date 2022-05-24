using Microsoft.AspNetCore.Mvc;
using TaskAPI.IService;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T, TRequest> : ControllerBase where T : class where TRequest : class
    {
        private IGenericService<T, TRequest> _genericService;

        public GenericController(IGenericService<T, TRequest> genericService)
        {
            _genericService = genericService;
        }

        // GET: api/<GenericController>
        [HttpGet("{query}/{currentPage}/{itemsPerPage}")]
        public List<T> GetAllPaginated(string? query = null, int currentPage = 0, int itemsPerPage = 10)
        {
            return _genericService.GetAllPaginated(query, currentPage, itemsPerPage);
        }
        [HttpGet]
        public List<T> GetAll()
        {
            return _genericService.GetAll();
        }
        // GET api/<GenericController>/5
        [HttpGet("{id}")]
        public T Get(int id)
        {
            return _genericService.GetById(id);
        }

        // POST api/<GenericController>
        [HttpPost]
        public List<T> Post([FromBody] TRequest value)
        {
            return _genericService.Insert(value);
        }

        // PUT api/<GenericController>/5
        [HttpPut("{id}")]
        public List<T> Put(int id, [FromBody] TRequest value)
        {
            return _genericService.Update(id, value);
        }

        // DELETE api/<GenericController>/5
        [HttpDelete("{id}")]
        public List<T> Delete(int id)
        {
            return _genericService.Delete(id);
        }
    }
}
