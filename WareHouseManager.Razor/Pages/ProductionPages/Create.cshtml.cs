using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WareHouseManager.Razor.Dto_s;
using WareHouseManager.Razor.Models;
using WareHouseManager.Razor.Service;


namespace WareHouseManager.Razor.Pages.ProductionPages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ProductionService _productionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ProductionService productionService,
                           UserManager<ApplicationUser> userManager)
        {
            _productionService = productionService;
            _userManager = userManager;
        }

        public string Messages { get; set; }
        private string UserId { get; set; }

        [BindProperty]
        public ProductionDto productionDto { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Messages = "Model InValid";
                Console.WriteLine("Date : "+DateTime.Now+" --Model InValid:Create production");
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                return Page();
            }
            Messages = "Model Valid";
            Console.WriteLine("Date : " + DateTime.UtcNow + " --Model Valid");
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                UserId = user.Id;
            }

            var resp = await _productionService.Create(productionDto, UserId);
            if (resp=="1")
            {
                return RedirectToPage("/ProductionPages/Index");
            }

            Messages = resp;
            return Page();
           
        }

    }
}
