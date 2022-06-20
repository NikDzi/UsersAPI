using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using TaskAPI.RequestModels;
namespace TaskAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [StringLength(20)]
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }

        public User(UserRequest userRequest)
        {
            FirstName = userRequest.FirstName;
            LastName = userRequest.LastName;
            UserName = userRequest.UserName;
            Email = userRequest.Email;
            Status = userRequest.Status;
            PermissionId = userRequest.PermissionId;
            Salt = GenerateSalt();
            Password = GenerateHash(Salt, Password);
        }

        public User()
        {
        }

        public void updateUser(UserRequest userRequest)
        {
            FirstName = userRequest.FirstName;
            LastName = userRequest.LastName;
            UserName = userRequest.UserName;
            Email = userRequest.Email;
            Status = userRequest.Status;
            PermissionId = userRequest.PermissionId;
            Salt = GenerateSalt();
            Password = GenerateHash(Salt, Password);
        }
        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA512");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }
    }
}
