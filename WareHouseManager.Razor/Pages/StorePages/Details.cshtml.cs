using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WareHouseManager.Razor.Dto_s;
using WareHouseManager.Razor.Service;


namespace WareHouseManager.Razor.Pages.StorePages
{
    public class DetailsModel : PageModel
    {
        private readonly StoreService _storeService;

        public DetailsModel(StoreService storeService)
        {
            _storeService=storeService;
        }

        [BindProperty]
        public string Messages { get; set; }

        [BindProperty]
        public updateStoreDto Store { get; set; } 
        public async Task<IActionResult> OnGet(int? id)
        {
            try
            {
                if (id==null)
                {
                    Messages = "Not Found!";
                    return Page();
                }
                Store = await _storeService.GetStoreByIdWitnName(id);
                return Page();
            }
            catch (Exception e)
            {

                Messages = e.ToString();
                return Page();
            }

        }
    }
}
