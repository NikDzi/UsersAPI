using Microsoft.AspNetCore.Mvc;
using TaskAPI.Controllers;
using TaskAPI.Data;
using TaskAPI.Exceptions;
using TaskAPI.IService;
using TaskAPI.Models;
using TaskAPI.RequestModels;

namespace TaskAPI.Service
{
    public class PermissionService : IGenericService<Permission, PermissionRequest, LoginRequest>
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
            {
                throw new PermissionNotFoundException(id);
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
                throw new PermissionNotFoundException(id);
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

        public string Login(LoginRequest item)
        {
            throw new NotImplementedException();
        }

        public List<Permission> Update(int id, PermissionRequest item)
        {
            var updatePermission = _context.Permissions.Find(id);
            if (updatePermission == null)
            {
                throw new PermissionNotFoundException(id);
            }
            updatePermission.updatePermission(item);
            _context.SaveChanges();
            return GetAll();
        }
    }
}
