using System.ComponentModel.DataAnnotations;

namespace TaskAPI.RequestModels
{
    public class PermissionRequest
    {
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
    }
}
