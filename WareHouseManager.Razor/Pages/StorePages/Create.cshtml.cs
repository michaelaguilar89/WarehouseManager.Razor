using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WareHouseManager.Razor.Dto_s;
using WareHouseManager.Razor.Models;
using WareHouseManager.Razor.Service;


namespace WareHouseManager.Razor.Pages.StorePages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly StoreService _storeService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateModel(StoreService storeService,
                          UserManager<ApplicationUser> userManager)
        {
            _storeService = storeService;
            _userManager = userManager;
        }


        public string UserId { get; set; }
       // [BindProperty]
        public string? Messages { get; set; }

        [BindProperty]
        public StoreDto store { get; set; }
        public async void OnGetAsync()
        {
            try
            {

            }
            catch (Exception e)
            {

             
                Console.WriteLine("Error : " + e.Message);
              
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Messages = "Model Invalid!";
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                return Page();
            }
            
            
            
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    UserId = user.Id;
                }
              /*  var status = await _storeService.FindProductByName(store.ProductName);
                if (status==true)
                {
                    Messages = " Error ! ," + store.ProductName + " ya existe!";
                    return Page();
                }*/
                var resp = await _storeService.CreateStore(store, UserId);
                if (resp == "1")
                {
                    return RedirectToPage("/StorePages/Index");
                }
                Messages = resp;
                return Page();


            }
            catch (Exception e)  
            {

                Messages = e.Message;
                Console.WriteLine("Error : " + e.Message);
                return Page();
            }
                      

            
         

        }
    }
}
