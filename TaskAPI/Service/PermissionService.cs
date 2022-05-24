using TaskAPI.Data;
using TaskAPI.IService;
using TaskAPI.Models;
using TaskAPI.RequestModels;

namespace TaskAPI.Service
{
    public class PermissionService : IGenericService<Permission, PermissionRequest>
    {
        private readonly DataContext _context;

        public PermissionService(DataContext context)
        {
            _context = context;
        }

        public List<Permission> Delete(int id)
        {
            var permissionToRemove = _context.Permissions.FirstOrDefault(x => x.Id == id);
            if (permissionToRemove == null)
            {   // implement exception handler
                return null;
            }
            _context.Permissions.Remove(permissionToRemove);
            _context.SaveChanges();
            return GetAll();
        }
        public List<Permission> GetAll()
        {
            return _context.Permissions.ToList();
        }
        public List<Permission> GetAllPaginated(string? query = null, int currentPage = 0, int itemsPerPage = 10)
        {
            var queryable = _context.Permissions;
            return queryable.Skip(currentPage * itemsPerPage).Take(itemsPerPage).ToList();
        }

        public Permission GetById(int id)
        {
            var returnPermission = _context.Permissions.Find(id);
            if (returnPermission == null)
            {
                //implement exception handler
                return null;
            }
            return returnPermission;
        }

        public List<Permission> Insert(PermissionRequest item)
        {
            var newPermission = new Permission(item);
            _context.Permissions.Add(newPermission);
            _context.SaveChanges();
            return GetAll();
        }

        public List<Permission> Update(int id, PermissionRequest item)
        {
            var updatePermission = _context.Permissions.Find(id);
            if (updatePermission == null)
            {
                //implement exception handler
                return null;
            }
            updatePermission.updatePermission(item);
            _context.SaveChanges();
            return GetAll();
        }

    }
}
