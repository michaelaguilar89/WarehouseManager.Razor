using Microsoft.AspNetCore.Identity;

namespace WareHouseManager.Razor.Models
{
    public class ApplicationUser :IdentityUser
    {
        public DateTime CreationTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public bool IsActived { get; set; }
    }
}
