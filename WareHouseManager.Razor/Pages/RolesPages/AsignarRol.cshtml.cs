using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WareHouseManager.Razor.Models;

namespace WareHouseManager.Razor.Pages.RolesPages
{
    [Authorize(Roles ="Administrator")]
    public class AsignarRolModel : PageModel
    {
       
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AsignarRolModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        public string UserId { get; set; }

        [BindProperty]
        public string RoleName { get; set; }

        public string Messages { get; set; }
        public string MessagesOk { get; set; }
        public SelectList Users { get; set; }
        public SelectList Roles { get; set; }
        public async Task<IActionResult> OnGet()
        {

            Users = new SelectList(await _userManager.Users.ToListAsync(), "Id", "UserName");
            Roles = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            if (Users!=null && Roles!=null)
            {
                return Page();
            }
            Messages = "Users or Roles not found!!!";
            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, RoleName);
                    if (result.Succeeded)
                    {
                        Users = new SelectList(_userManager.Users.ToList(), "Id", "UserName");
                        Roles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
                        MessagesOk = "Status ok";
                        return RedirectToPage("/Identity/Account/Manage");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    Messages = result.Errors.ToList().ToString();
                    return Page();
                }
            }

            
            return Page();
        }
    }


   
}
