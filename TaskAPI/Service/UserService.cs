using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskAPI.Data;
using TaskAPI.Exceptions;
using TaskAPI.IService;
using TaskAPI.Models;
using TaskAPI.RequestModels;

namespace TaskAPI.Service
{
    public class UserService : IGenericService<User, UserRequest, LoginRequest>
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<User> Delete(int id)
        {
            var userToRemove = _context.Users.FirstOrDefault(x => x.Id == id);
            if (userToRemove == null)
            {
                throw new UserNotFoundException(id);
            }
            _context.Users.Remove(userToRemove);
            _context.SaveChanges();
            return GetAll();
        }
        public List<User> GetAll()
        {      // will populate database on first get call which is executed on angular app startup
            var currentuser = string.Empty;
            if (_httpContextAccessor.HttpContext!=null)
            {
                currentuser= _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            if (!_context.Users.Any())
            {
                SeedDataBase();
            }
            User usr = _context.Users.FirstOrDefault();
            var pass = usr.Password;
            var generated = User.GenerateHash(usr.Salt, "password");
            bool result = pass.SequenceEqual(generated);
            return _context.Users.ToList();
        }
        public List<User> GetAllPaginated(string? query = null, int currentPage = 0, int itemsPerPage = 10)
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
            var returnUser = _context.Users.Find(id);
            if (returnUser == null)
            {
                throw new UserNotFoundException(id);
            }
            return returnUser;
        }

        public List<User> Insert(UserRequest item)
        {
            var newUser = new User(item);
            var permission = _context.Permissions.Find(item.PermissionId);
            if (permission == null)
            {
                permission = _context.Permissions.Find(1);
            }
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
                throw new UserNotFoundException(id);
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
            for (int i = 1; i < 25; i++)
            {
                var seededUser = new User()
                {
                    FirstName = "SeededUser " + i,
                    LastName = "Lastname",
                    Email = "SeededUser" + i + "@mail.com",
                    Status = "Default",
                    UserName = "8CharsRequired " + i,
                    Password = pass,
                    Salt = salt,
                    PermissionId = 1,
                    Permission = permission
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

        public string Login(LoginRequest item)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.Equals(item.Username));
            if (user == null)
            {
                throw new NotFoundException("Username does not exist");
            }
            var hash = User.GenerateHash(user.Salt, item.Password);
            if (!user.Password.SequenceEqual(hash))
            {
                throw new BadRequestException("Wrong password");
            }
            var token = CreateToken(user);
            return token;
        }

        private string CreateToken(User user)
        {
            var role = _context.Permissions.Find(user.PermissionId);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,role.Code)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            var returnjwt = '"' + jwt + '"';
            return returnjwt;
        }
    }
}
