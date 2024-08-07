using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WareHouseManager.Razor.Dto_s;
using WareHouseManager.Razor.Models;
using WareHouseManager.Razor.Service;


namespace WareHouseManager.Razor.Pages.ProductionPages
{
    public class EditModel : PageModel
    {
        private readonly ProductionService _productionService;
        private readonly UserManager<ApplicationUser> userManager;

        public EditModel(ProductionService productionService, UserManager<ApplicationUser> userManager)
        {
            _productionService = productionService;
            this.userManager = userManager;
        }
        public string UserId { get; set; }
        public string Messages { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        [BindProperty]
        public UpdateProductionDto productionDto { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            try
            {
                if (id != null)
                {
                    Id = id;
                    productionDto = await _productionService.GetProductionsById(id);

                }
                else
                {
                    Messages = "Record Not found!";
                }
             
                return Page();

            }
            catch (Exception e)
            {

                Messages = e.Message;
                return Page();
            }
           

        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
               
                if (!ModelState.IsValid)
                {
                    Messages = "Model Invalid";
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            Console.WriteLine("Error : " + error.ErrorMessage);
                        }
                    }
                    return Page();
                }

                var user = await this.userManager.GetUserAsync(User);
                if (user != null)
                {
                    UserId = user.Id;
                }

                var resp = await _productionService.Update(productionDto, UserId);
                if (resp == "1")
                {
                    return RedirectToPage("/ProductionPages/Index");
                }
                if (resp == "0")
                {
                    Messages = "Internal Error";
                    return Page();
                }
                else
                {
                    Messages = resp;
                }

                return Page();
            }
            catch (Exception e)
            {
                Messages = e.Message;
                return Page();
            }
            
        }


    }
}
