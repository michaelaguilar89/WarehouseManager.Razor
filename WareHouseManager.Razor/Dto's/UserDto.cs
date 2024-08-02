using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;

namespace WareHouseManager.Razor.Dto_s
{
    public class UserDto
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        [Required]
        public DateTime LastLoginTime { get; set; }
        [Required]
        public bool IsActived { get; set; }

       
    }
}
