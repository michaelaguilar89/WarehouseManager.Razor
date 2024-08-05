using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using WareHouseManager.Razor.Data;
using WareHouseManager.Razor.Dto_s;
using WareHouseManager.Razor.Models;
using WareHouseManager.Razor.Service;


namespace WareHouseManager.Razor.Pages.ProductionPages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ProductionService _productionService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ProductionService productionService,
                           UserManager<ApplicationUser> userManager,
                           ApplicationDbContext context)
        {
            _productionService = productionService;
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public List<ResultProductionDto>? list { get; set; } = default;
        public string Messages { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? search { get; set; }
        [BindProperty(SupportsGet = true)]
        public int records { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public DateTime? searchDate { get; set; }

        public async Task OnGetAsync(string? searchString, DateTime? searchDateValue)
        {
            try
            {

                searchDate = searchDateValue;
                search = searchString;
                Console.WriteLine("Production Pages -Index : searchString : " + search + ", searchDate : " + searchDate);
                if (search != null && searchDate.HasValue)
                {
                    //list = await _productionService.GetProductionsByNameOrBatchAndDate(search, startDate.Value);
                }
                else
                {
                    if (searchDate.HasValue)
                    {
                        list = await _productionService.GetProductionsByDate(searchDate.Value);
                    }
                    else
                    {
                        if (search != null)
                        {
                            list = await _productionService.GetProductionsByNameOrBatch(search);

                        }
                        else 
                        {
                            list = await _productionService.GetProductions();
                        }

                    }
                    
                }

               
                
              
                Console.WriteLine("Date : " + DateTime.Now + ", Consulta OK en production index " + list.ToJson());
            }
            catch (Exception e)
            {
                Messages = e.Message;
                Console.WriteLine("Date : " + DateTime.Now + ", Error en Production-Index, Error: " + e.Message);
            }



        }
    }
}
