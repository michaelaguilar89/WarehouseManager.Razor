// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using WareHouseManager.Razor.Models;

namespace WareHouseManager.Razor.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Administrator")]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }

          public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            //[Required]
           // public string Code { get; set; } = "test";

        }
        public string Messages { get; set; }
        public string MessagesOk { get; set; }
        public async Task<IActionResult> OnGet( string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Messages="User ID not provided.";
                return Page(); 
                    
            }
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                Messages = "User not found";
                return Page();
            }
            Input.Email = user.Email;
            UserId = user.Id;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //Input.Code = "test";
            if (!ModelState.IsValid)
            {
                Messages = "Model Invalid";
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine("Date :"+DateTime.UtcNow+", Error on ResetPassword: " + error.ErrorMessage);
                    }
                }
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                Messages = "Invalid User";
                return Page();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, Input.Password);
            if (result.Succeeded)
            {
                MessagesOk = "Password has update!";
                return Page();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
