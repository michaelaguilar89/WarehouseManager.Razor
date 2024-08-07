using Microsoft.AspNetCore.DataProtection;
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
        private readonly EncryptedSevice encryptedSevice;

        public StoreService(ApplicationDbContext context,EncryptedSevice encryptedSevice)
        {
             _context = context;
            this.encryptedSevice = encryptedSevice;
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
                        actualData.ProductName= this.encryptedSevice.Protect(storeDto.ProductName);
                        actualData.ProductNameHash = this.encryptedSevice.HashString(storeDto.ProductName);
                       
                        actualData.BatchHash=this.encryptedSevice.HashString(storeDto.Batch);
                        actualData.Batch = this.encryptedSevice.Protect(storeDto.Batch);
                        actualData.TotalQuantity = totalQuantity;
                        actualData.ActualQuantity = ActualQuantity;
                        actualData.CreationTime = DateTime.SpecifyKind(storeDto.CreationTime, DateTimeKind.Utc);
                        actualData.Description = this.encryptedSevice.Protect(storeDto.Description);
                        actualData.Comments = this.encryptedSevice.Protect(storeDto.Comments);
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
                var BatchHash = this.encryptedSevice.HashString(storeDto.Batch);
                var exist = await _context.Stores.FirstOrDefaultAsync(
                    x => x.BatchHash.Equals(BatchHash)
                    );
                if (exist!=null)
                {
                    return "Wrong , Lote debe ser unico,ya existe!";
                }
                else
                {
                    Store store = new();
                    store.ProductNameHash = this.encryptedSevice.HashString(storeDto.ProductName);
                    store.ProductName = this.encryptedSevice.Protect(storeDto.ProductName);
                    store.BatchHash = BatchHash;
                    store.Batch = this.encryptedSevice.Protect(storeDto.Batch);
                    store.TotalQuantity = storeDto.Quantity;
                    store.ActualQuantity = storeDto.Quantity;
                    store.CreationTime = DateTime.SpecifyKind(storeDto.CreationTime, DateTimeKind.Utc);
                    store.Description = this.encryptedSevice.Protect(storeDto.Description);
                    store.Comments = this.encryptedSevice.Protect(storeDto.Comments);
                    store.UserIdCreation = secret;
                    store.ModificationAt = null;
                    //  store.UserIdModification = secret;
                    await _context.Stores.AddAsync(store);
                    await _context.SaveChangesAsync();
                    return "1";
                }
                
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }

        public async Task<List<ResultStoreDto>> getStoresByNameOrBatch(string search)
        {
            var searchingHash = this.encryptedSevice.HashString(search);

                var stores = await _context.Stores
      .Include(store => store.UserCreation) // Incluir el usuario de creación
      .Include(store => store.UserModification) // Incluir el usuario de modificación
      .Where(z=>z.ProductNameHash.Equals(searchingHash) || z.BatchHash.Equals(searchingHash))
      .Select(store => new ResultStoreDto
      {
          Id = store.Id,
          ProductName = this.encryptedSevice.UnProtect(store.ProductName),
          Batch =this.encryptedSevice.UnProtect( store.Batch),
          TotalQuantity = store.TotalQuantity,
          ActualQuantity = store.ActualQuantity,
          CreationTime = store.CreationTime,
          ModificationAt = store.ModificationAt,
          Description =this.encryptedSevice.UnProtect( store.Description),
          Comments = this.encryptedSevice.UnProtect( store.Comments),
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
            var searchingHah = this.encryptedSevice.HashString(search);
            var stores = await _context.Stores
  .Include(store => store.UserCreation) // Incluir el usuario de creación
  .Include(store => store.UserModification) // Incluir el usuario de modificación
  .Where(
         z => z.CreationTime.Date == searchDate.GetValueOrDefault().ToUniversalTime().Date
               && z.BatchHash.Equals(searchingHah)
         )
  .Select(store => new ResultStoreDto
  {
      Id = store.Id,
      ProductName = this.encryptedSevice.UnProtect(store.ProductName),
      Batch = this.encryptedSevice.UnProtect(store.Batch),
      TotalQuantity = store.TotalQuantity,
      ActualQuantity = store.ActualQuantity,
      CreationTime = store.CreationTime,
      ModificationAt = store.ModificationAt,
      Description = this.encryptedSevice.UnProtect(store.Description),
      Comments = this.encryptedSevice.UnProtect(store.Comments),
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
      ProductName = this.encryptedSevice.UnProtect(store.ProductName),
      Batch = this.encryptedSevice.UnProtect(store.Batch),
      TotalQuantity = store.TotalQuantity,
      ActualQuantity = store.ActualQuantity,
      CreationTime = store.CreationTime,
      ModificationAt = store.ModificationAt,
      Description = this.encryptedSevice.UnProtect(store.Description),
      Comments = this.encryptedSevice.UnProtect(store.Comments),
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
          
            
             var stores = await _context.Stores
      .Include(store => store.UserCreation) // Incluir el usuario de creación
      .Include(store => store.UserModification) // Incluir el usuario de modificación
      .Select(store => new ResultStoreDto
      {
          Id = store.Id,
          ProductName =this.encryptedSevice.UnProtect( store.ProductName),
          Batch = this.encryptedSevice.UnProtect(store.Batch),
          TotalQuantity = store.TotalQuantity,
          ActualQuantity = store.ActualQuantity,
          CreationTime = store.CreationTime,
          ModificationAt = store.ModificationAt, 
          Description = this.encryptedSevice.UnProtect(store.Description),
          Comments = this.encryptedSevice.UnProtect(store.Comments),
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
             ProductName =this.encryptedSevice.UnProtect(store.ProductName),
             Batch = this.encryptedSevice.UnProtect(store.Batch),
             TotalQuantity = store.TotalQuantity,
             ActualQuantity = store.ActualQuantity,
             operation = "+",
             UpdateQuantity = 0,
             CreationTime = store.CreationTime,
             ModificacionTime = store.ModificationAt,
             Description =this.encryptedSevice.UnProtect( store.Description),
             Comments = this.encryptedSevice.UnProtect(store.Comments),
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
