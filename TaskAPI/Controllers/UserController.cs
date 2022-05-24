using Microsoft.AspNetCore.Mvc;
using TaskAPI.IService;
using TaskAPI.Models;
using TaskAPI.RequestModels;
using TaskAPI.Service;

namespace TaskAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : GenericController<User,UserRequest>
    {
        public UserController(IGenericService<User,UserRequest> genericService) : base(genericService)
        {
        }
    }
}
