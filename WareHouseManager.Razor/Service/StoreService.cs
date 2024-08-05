using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using System.Threading.Tasks.Dataflow;
using WareHouseManager.Razor.Data;
using WareHouseManager.Razor.Dto_s;
using WareHouseManager.Razor.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WareHouseManager.Razor.Service
{
    public class StoreService
    {
        private readonly ApplicationDbContext _context;

        public StoreService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> UpdateStore(updateStoreDto storeDto, string secret)
        {
            try
            {
                string mesage;
                decimal ActualQuantity = 0;
                decimal totalQuantity = 0;
                    if (storeDto.operation.Equals("+")||
                    storeDto.operation.Equals("-")||
                    storeDto.operation.Equals("0"))
                {
                    var actualData = await _context.Stores.FindAsync(storeDto.Id);
                    if (actualData != null)
                    {


                        actualData.ProductName = storeDto.ProductName;
                        actualData.Batch = storeDto.Batch;
                        switch (storeDto.operation)
                        {
                            case "+":
                                totalQuantity = actualData.TotalQuantity + storeDto.UpdateQuantity;
                                ActualQuantity = actualData.ActualQuantity + storeDto.UpdateQuantity;
                            break;
                            case "-":
                                totalQuantity = actualData.TotalQuantity + storeDto.UpdateQuantity;
                                ActualQuantity = actualData.ActualQuantity + storeDto.UpdateQuantity;
                                break;
                            case "0":
                                totalQuantity = actualData.TotalQuantity;
                                ActualQuantity = actualData.ActualQuantity;
                                break;
                            default:
                                mesage = "Invaid Operation";
                             break;
                        }

                        actualData.TotalQuantity = totalQuantity;
                        actualData.ActualQuantity = ActualQuantity;
                        actualData.CreationTime = DateTime.SpecifyKind(storeDto.CreationTime, DateTimeKind.Utc);
                        actualData.Description = storeDto.Description;
                        actualData.Comments = storeDto.Comments;
                        actualData.UserIdCreation = storeDto.UserIdCreation;
                        actualData.ModificationAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                        actualData.UserIdModification = secret;
                        _context.Stores.Update(actualData);
                        await _context.SaveChangesAsync();
                       return mesage="1";

                    }
                   return mesage = "0";
                }
                return mesage = "Invalid Operation";
                
            }
            catch (Exception e )
            {

                return e.Message;
            }
        }
        public async Task<string> CreateStore(StoreDto storeDto,string secret)
        {
            try
            {
                Store store = new();
                store.ProductName = storeDto.ProductName;
                store.Batch = storeDto.Batch;
                store.TotalQuantity = storeDto.Quantity;
                store.ActualQuantity = storeDto.Quantity;
                store.CreationTime = DateTime.SpecifyKind(storeDto.CreationTime, DateTimeKind.Utc);
                store.Description = storeDto.Description;
                store.Comments = storeDto.Comments;
                store.UserIdCreation = secret;
                store.ModificationAt = null;
              //  store.UserIdModification = secret;
                await _context.Stores.AddAsync(store);
                await _context.SaveChangesAsync();
                return "1";
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }

        public async Task<List<ResultStoreDto>> getStoresByNameOrBatch(string search)
        {
                var stores = await _context.Stores
      .Include(store => store.UserCreation) // Incluir el usuario de creación
      .Include(store => store.UserModification) // Incluir el usuario de modificación
      .Where(z=>z.ProductName.ToLower().Contains(search.ToLower())||
             z.Batch.ToLower().Contains(search.ToLower()))
      .Select(store => new ResultStoreDto
      {
          Id = store.Id,
          ProductName = store.ProductName,
          Batch = store.Batch,
          TotalQuantity = store.TotalQuantity,
          ActualQuantity = store.ActualQuantity,
          CreationTime = store.CreationTime,
          ModificationAt = store.ModificationAt,
          Description = store.Description,
          Comments = store.Comments,
          UserIdCreation = store.UserIdCreation,
          UserNameCreation = store.UserCreation != null ? store.UserCreation.UserName : "Unknown", // Manejo de potencial null
          UserIdModification = store.UserIdModification,
          UserNameModification = store.UserModification != null ? store.UserModification.UserName : "Unknown" // Manejo de potencial null
          })
          .ToListAsync();



           return stores;            
        }
        public async Task<List<ResultStoreDto>> getStoresByNameOrBatchAndDate(string search,DateTime? searchDate)
        {
            var stores = await _context.Stores
  .Include(store => store.UserCreation) // Incluir el usuario de creación
  .Include(store => store.UserModification) // Incluir el usuario de modificación
  .Where(z => z.CreationTime.Equals(searchDate) || z.ProductName.ToLower().Contains(search.ToLower()) ||
         z.Batch.ToLower().Contains(search.ToLower()))
  .Select(store => new ResultStoreDto
  {
      Id = store.Id,
      ProductName = store.ProductName,
      Batch = store.Batch,
      TotalQuantity = store.TotalQuantity,
      ActualQuantity = store.ActualQuantity,
      CreationTime = store.CreationTime,
      ModificationAt = store.ModificationAt,
      Description = store.Description,
      Comments = store.Comments,
      UserIdCreation = store.UserIdCreation,
      UserNameCreation = store.UserCreation != null ? store.UserCreation.UserName : "Unknown", // Manejo de potencial null
      UserIdModification = store.UserIdModification,
      UserNameModification = store.UserModification != null ? store.UserModification.UserName : "Unknown" // Manejo de potencial null
  })
      .ToListAsync();



            return stores;
        }

        public async Task<List<ResultStoreDto>> getStoresByDate(DateTime? searchDate)
         {
           
             var stores = await _context.Stores
  .Include(store => store.UserCreation) // Incluir el usuario de creación
  .Include(store => store.UserModification) // Incluir el usuario de modificación
   .Where(z => z.CreationTime.Date ==searchDate.GetValueOrDefault().ToUniversalTime().Date) // Comparación por rango de fecha
  .Select(store => new ResultStoreDto
  {
      Id = store.Id,
      ProductName = store.ProductName,
      Batch = store.Batch,
      TotalQuantity = store.TotalQuantity,
      ActualQuantity = store.ActualQuantity,
      CreationTime = store.CreationTime,
      ModificationAt = store.ModificationAt,
      Description = store.Description,
      Comments = store.Comments,
      UserIdCreation = store.UserIdCreation,
      UserNameCreation = store.UserCreation != null ? store.UserCreation.UserName : "Unknown", // Manejo de potencial null
      UserIdModification = store.UserIdModification,
      UserNameModification = store.UserModification != null ? store.UserModification.UserName : "Unknown" // Manejo de potencial null
  })
      .ToListAsync();

           
            
            // Imprimir el número de resultados para depuración
            Console.WriteLine($"Number of results: {stores.Count}");

            return stores;

        }
        public async Task<List<ResultStoreDto>> GetStoresWithUsernamesAsync()
        {
           /* var stores = await (from store in _context.stores
                                join userCreation in _context.Users on store.UserIdCreation equals userCreation.Id into uc
                                from userCreation in uc.DefaultIfEmpty()
                                join userModification in _context.Users on store.UserIdModification equals userModification.Id into um
                                from userModification in um.DefaultIfEmpty()
                                select new ResultStoreDto
                                {
                                    Id = store.Id,
                                    ProductName = store.ProductName,
                                    Batch = store.Batch,
                                    Quantity = store.Quantity,
                                    CreationTime = store.CreationTime,
                                    ModificacionTime = store.ModificacionTime.Value,
                                    Description = store.Description,
                                    Comments = store.Comments,
                                    UserIdCreation = store.UserIdCreation,
                                    UserNameCreation = userCreation.UserName ,
                                    UserIdModification = store.UserIdModification,
                                    UserNameModification = userModification.UserName
                                }).ToListAsync();*/
            
             var stores = await _context.Stores
      .Include(store => store.UserCreation) // Incluir el usuario de creación
      .Include(store => store.UserModification) // Incluir el usuario de modificación
      .Select(store => new ResultStoreDto
      {
          Id = store.Id,
          ProductName = store.ProductName,
          Batch = store.Batch,
          TotalQuantity = store.TotalQuantity,
          ActualQuantity = store.ActualQuantity,
          CreationTime = store.CreationTime,
          ModificationAt = store.ModificationAt, 
          Description = store.Description,
          Comments = store.Comments,
          UserIdCreation = store.UserIdCreation,
          UserNameCreation = store.UserCreation != null ? store.UserCreation.UserName : "Unknown", // Manejo de potencial null
          UserIdModification = store.UserIdModification,
          UserNameModification = store.UserModification != null ? store.UserModification.UserName : "Unknown" // Manejo de potencial null
      })
      .ToListAsync();

            

            return stores;
        }
        public async Task<List<Store>> GetStoresAsync()
        {
            try
            {
               var result = await _context.Stores.ToListAsync();
                return result;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public async Task<updateStoreDto> GetStoreByIdWitnName(int? id)
        {
            var stores = await _context.Stores
         .Where(store => store.Id == id) // Filtra por el id proporcionado
         .Include(store => store.UserCreation) // Incluye el usuario de creación
         .Include(store => store.UserModification) // Incluye el usuario de modificación
         .Select(store => new updateStoreDto
         {
             Id = store.Id,
             ProductName = store.ProductName,
             Batch = store.Batch,
             TotalQuantity = store.TotalQuantity,
             ActualQuantity = store.ActualQuantity,
             operation = "+",
             UpdateQuantity = 0,
             CreationTime = store.CreationTime,
             ModificacionTime = store.ModificationAt,
             Description = store.Description,
             Comments = store.Comments,
             UserIdCreation = store.UserIdCreation,
             UserNameCreation = store.UserCreation != null ? store.UserCreation.UserName : "Unknown",
             UserIdModification = store.UserIdModification,
             UserNameModification = store.UserModification != null ? store.UserModification.UserName : "---"
         })
         .FirstOrDefaultAsync();

            return stores;


        }
        public async Task<Store> GetStoreById(int? id)
        {

            if (id==null)
            {
                return null;
            }
                var st = await _context.Stores.FindAsync(id);
                return st;
                       
        }

        private async Task<bool> Exist(int? id)
        {
            try
            {
                var item = false;
                item = await _context.Stores.AnyAsync(x => x.Id==id);
                return item;
            }
            catch (Exception)
            {

                return false;
            }
        }
       

        public async Task<bool> FindProductByName(string name){
            try
            {
                var a = await _context.Stores.AnyAsync(x => x.ProductName == name);
                if (a == true)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
