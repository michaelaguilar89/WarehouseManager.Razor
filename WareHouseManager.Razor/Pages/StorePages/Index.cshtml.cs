using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WareHouseManager.Razor.Dto_s;
using WareHouseManager.Razor.Service;


namespace WareHouseManager.Razor.Pages.StorePages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly StoreService _storeService;

        public IndexModel(StoreService storeService)
        {
            _storeService = storeService;
        }
        
       // [BindProperty]
        public string Messages { get; set; }
        [BindProperty]
        public List<ResultStoreDto> result { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? search { get; set; }
        [BindProperty(SupportsGet = true)]
        public int records { get; set; } = 0;
        public DateTime? searchDate { get; set; }
        public async Task<IActionResult> OnGet(string? searchString, DateTime? searchDateValue)
        {

            
            try
            {

                searchDate = searchDateValue;
                search = searchString;

                if (searchDate != null)
                 {
                    
                     result = await _storeService.getStoresByDate(searchDate);
                 }
                 else
                 {
                     if (searchDateValue != null && search != null)
                     {
                       
                         result = await _storeService.getStoresByNameOrBatchAndDate(searchString, searchDate);
                     }
                     else
                     {
                         if (searchString != null)
                         {
                             
                             result = await _storeService.getStoresByNameOrBatch(search);

                         }
                         else {
                             result = await _storeService.GetStoresWithUsernamesAsync();
                         }
                     }




                 }


                 if (result == null)
             {
                 Messages = "Data not found, try agin...";

                }
                else
                {
                   // records = result.Count();
                }
                
            return Page();
            }
            catch (Exception e)
            {
                Messages = "Error : " + e.Message;
                return Page();
            }
        }
    }


}
