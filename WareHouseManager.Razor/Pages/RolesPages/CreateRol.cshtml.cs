using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WareHouseManager.Razor.Pages.RolesPages
{
    [Authorize(Roles ="Administrator")]
    public class CreaateRolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public CreaateRolesModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        [BindProperty]
        [Required]
        public string RoleName { get; set; }


        public void OnGet()
        {
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var roleExist = await _roleManager.RoleExistsAsync(RoleName);
            if (!roleExist)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(RoleName));
                if (result.Succeeded)
                {
                    return RedirectToPage("/Identity/Account/Manage");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Role already exists.");
            }

            return Page();
        }
    }

}
