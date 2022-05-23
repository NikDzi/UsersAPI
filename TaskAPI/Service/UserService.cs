using TaskAPI.Data;
using TaskAPI.IService;
using TaskAPI.Models;
using TaskAPI.RequestModels;

namespace TaskAPI.Service
{
    public class UserService : IGenericService<User,UserRequest>
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public List<User> Delete(int id)
        {
            var userToRemove = _context.Users.FirstOrDefault(x => x.Id == id);
            if (userToRemove == null)
            {   // implement exception handler
                return null;
            }
            _context.Users.Remove(userToRemove);
            _context.SaveChanges();
            return GetAll();
        }

        public List<User> GetAll(string? query=null, int currentPage=0, int itemsPerPage = 10)
        {      // will populate database on first get call which is executed on angular app startup
            if (!_context.Users.Any())
            {
                SeedDataBase();
            }
            var queryable = _context.Users;
            return queryable.Skip(currentPage * itemsPerPage).Take(itemsPerPage).ToList();
        }

        public User GetById(int id)
        {
            var returnUser= _context.Users.Find(id);
            if (returnUser == null)
            {
                //implement exception handler
                return null;
            }
            return returnUser;
        }

        public List<User> Insert(UserRequest item)
        {
            var newUser = new User(item);
            var permission = _context.Permissions.Find(item.PermissionId);
            newUser.Permission = permission;
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return GetAll();
        }

        public List<User> Update(int id, UserRequest item)
        {
            var updateUser = _context.Users.Find(id);
            if (updateUser == null)
            {
                //implement exception handler
                return null;
            }
            updateUser.updateUser(item);
            var permission = _context.Permissions.Find(item.PermissionId);
            updateUser.Permission = permission;
            _context.SaveChanges();
            return GetAll();
        }

        public void SeedDataBase()
        {
            if (!_context.Permissions.Any())
            {
                SeedPermissions();

            }
            var salt = User.GenerateSalt();
            var pass = User.GenerateHash(salt, "password");
            var permission = _context.Permissions.Find(1);
            for (int i = 0; i < 25; i++)
            {
                var seededUser = new User() { 
                
                   FirstName = "SeededUser " + i,
                   LastName = "Lastname",
                   Email = "SeededUser" + i + "@mail.com",
                   Status = "Default",
                   UserName = "8CharsRequired " + i,
                   Password = pass,
                   PermissionId = 1,
                   Permission=permission
                };
                _context.Add(seededUser);
            }
            _context.SaveChanges();
        }
        public void SeedPermissions()
        {
            var permissions = new List<Permission> {
                new Permission
                {
                    Code="U1",
                    Description="Basic user permissions"
                },new Permission
                {
                    Code="M1",
                    Description="Moderator lvl 1 permission"
                },new Permission
                {
                    Code="M2",
                    Description="Moderator lvl 2 permission"
                },new Permission
                {
                    Code="A1",
                    Description="Admin permissions"
                },new Permission
                {
                    Code="A2",
                    Description="SuperAdmin permissions"
                },
            };
            foreach (var item in permissions)
            {
                _context.Permissions.Add(item);
            }
            _context.SaveChanges();
        }
    }
}
