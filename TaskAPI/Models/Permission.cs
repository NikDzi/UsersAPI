using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

    }
}
