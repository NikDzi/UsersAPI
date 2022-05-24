using System.ComponentModel.DataAnnotations;
using TaskAPI.RequestModels;

namespace TaskAPI.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        public Permission(PermissionRequest item)
        {
            Code = item.Code;
            Description = item.Description;
        }
        public Permission()
        {
        }
        public void updatePermission(PermissionRequest permissionRequest)
        {
            Code = permissionRequest.Code;
            Description=permissionRequest.Description;
        }
    }
}
