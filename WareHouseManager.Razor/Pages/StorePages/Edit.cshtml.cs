using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WareHouseManager.Razor.Dto_s;
using WareHouseManager.Razor.Models;
using WareHouseManager.Razor.Service;

namespace WareHouseManager.Razor.Pages.StorePages
{
    public class EditModel : PageModel
    {
        private readonly StoreService _storeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(StoreService storeService, UserManager<ApplicationUser> userManager)
        {
            _storeService = storeService;
            _userManager = userManager;
        }

        [BindProperty]
        public updateStoreDto StoreDto { get; set; } = default!;
        public string UserId { get; set; }
        public string operation { get; set; }

        public string Messages { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Messages = "Data not Found!";
                return Page();
            }

            var resultstoredto =  await _storeService.GetStoreByIdWitnName(id);
            if (resultstoredto == null)
            {
                return NotFound();
            }
            StoreDto = resultstoredto;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Messages = "Model Invalid";
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine("Error : "+error.ErrorMessage);
                    }
                }
                return Page();

            }
           
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    UserId = user.Id;
                }
                //StoreDto.ModificacionTime = DateTimeOffset.Now;
                var resp = await _storeService.UpdateStore(StoreDto, UserId);
                if (resp=="1")
                {
                    return RedirectToPage("/StorePages/Index");
                }
                if (resp=="0")
                {
                    Messages = "Internal Error";
                    return Page();
                }
                Messages = resp;
                return Page();

        }

        private string getSecret()
        {
            var user = _userManager.GetUserAsync(User);
            if (user != null)
            {
                return user.Id.ToString();
            }
            return null;
        }

      /*  private bool ResultStoreDtoExists(int id)
        {
            return _context.ResultStoreDto.Any(e => e.Id == id);
        }*/
    }
}
