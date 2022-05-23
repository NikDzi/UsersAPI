using System.ComponentModel.DataAnnotations;

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
        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }
    }
}
