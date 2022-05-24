using System.ComponentModel.DataAnnotations;

namespace TaskAPI.RequestModels
{
    public class UserRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Status { get; set; } = string.Empty;
        [Required]
        public int PermissionId { get; set; }
    }
}
