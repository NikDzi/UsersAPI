using Microsoft.AspNetCore.Mvc;
using TaskAPI.IService;
using TaskAPI.Models;
using TaskAPI.RequestModels;

namespace TaskAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PermissionController : GenericController<Permission, PermissionRequest>
    {
        public PermissionController(IGenericService<Permission, PermissionRequest> genericService) : base(genericService)
        {
        }
    }
}
