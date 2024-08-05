using Microsoft.EntityFrameworkCore;

using WareHouseManager.Razor.Data;
using WareHouseManager.Razor.Dto_s;
using WareHouseManager.Razor.Models;


namespace WareHouseManager.Razor.Service
{
    public class ProductionService
    {
        private readonly ApplicationDbContext _context;
        private readonly EncryptedSevice encryptedSevice;

        public ProductionService(ApplicationDbContext context, EncryptedSevice encryptedSevice)
        {
            _context = context;
            this.encryptedSevice = encryptedSevice;
        }


        public async Task<ResultProductionDto> GetProductionsById(int? id)
        {
            try
            {

                var data = await _context.Productions
                    .Include(user => user.UserCreation)//incluir el username create
                    .Include(user => user.UserModification)//incluir el username modification
                    .Where(x => x.Id == id)
                    .Select(p => new ResultProductionDto
                    {
                        Id = p.Id,
                        ProductName = this.encryptedSevice.UnProtect(p.ProductName),
                        Batch = this.encryptedSevice.UnProtect(p.Batch),
                        StoreId = p.StoreId,
                        Quantity = p.Quantity,
                        Tank = this.encryptedSevice.UnProtect(p.Tank),
                        FinalLevel = this.encryptedSevice.UnProtect(p.FinalLevel),
                        CreationTime = p.CreationTime,
                        ModificacionTime = p.ModificacionTime,
                        Comments = this.encryptedSevice.UnProtect(p.Comments),
                        UserIdCreation = p.UserIdCreation,
                        UserNameCreation = p.UserCreation.UserName != null ? p.UserCreation.UserName : "Unknown",
                        UserIdModification = p.UserIdModification != null ? p.UserCreation.UserName : "Unknown",
                        UserNameModification = p.UserModification.UserName != null ? p.UserModification.UserName : "Unknown" // Manejo de potencial null,
                      
                    }).FirstOrDefaultAsync();
                return data;

            }
            catch (Exception e)
            {

                Console.WriteLine("Date : " + DateTime.Now + " Error : " + e.Message);
                return null;
            }

        }

        public async Task<List<ResultProductionDto>> GetProductions()
        {
            try
            {
                var prod = await _context.Productions
                    .Include(user => user.UserCreation)//incluir el username create
                    .Include(user => user.UserModification)//incluir el username modification
                    .Select(p => new ResultProductionDto
                    {
                        Id = p.Id,

                        ProductName =this.encryptedSevice.UnProtect( p.ProductName),
                        Batch = this.encryptedSevice.UnProtect(p.Batch),
                        StoreId = p.StoreId,
                        Quantity = p.Quantity,
                        Tank = this.encryptedSevice.UnProtect(p.Tank),
                        FinalLevel = this.encryptedSevice.UnProtect(p.FinalLevel),
                        CreationTime = p.CreationTime,
                        ModificacionTime = p.ModificacionTime,
                        Comments = this.encryptedSevice.UnProtect(p.Comments),
                        UserIdCreation = p.UserIdCreation,
                        UserNameCreation = p.UserCreation.UserName != null ? p.UserCreation.UserName : "Unknown",
                        UserIdModification = p.UserIdModification != null ? p.UserCreation.UserName : "Unknown",
                        UserNameModification = p.UserModification.UserName != null ? p.UserModification.UserName : "Unknown" // Manejo de potencial null,
                      
                    }).ToListAsync();
                // await _context.DisposeAsync();
                return prod;

            }
            catch (Exception e)
            {

                Console.WriteLine("Date : " + DateTime.Now + " Error : " + e.Message);
                return null;
            }

        }

        public async Task<List<ResultProductionDto>> GetProductionsByNameOrBatch(string? search)
        {
            try
            {
                if (search!=null)
                {
                    var searchingHash = this.encryptedSevice.HashString(search);
                    var prod = await _context.Productions
                    .Include(user => user.UserCreation)//incluir el username create
                    .Include(user => user.UserModification)//incluir el username modification                   
                    .Where(z => z.ProductNameHash.Equals(searchingHash) ||
                                z.BatchHash.Equals(searchingHash))
                    .Select(p => new ResultProductionDto
                    {
                        Id = p.Id,
                        ProductName = this.encryptedSevice.UnProtect(p.ProductName),
                        Batch = this.encryptedSevice.UnProtect(p.Batch),
                        StoreId = p.StoreId,
                        Quantity = p.Quantity,
                        Tank = this.encryptedSevice.UnProtect(p.Tank),
                        FinalLevel = this.encryptedSevice.UnProtect(p.FinalLevel),
                        CreationTime = p.CreationTime,
                        ModificacionTime = p.ModificacionTime,
                        Comments = this.encryptedSevice.UnProtect(p.Comments),
                        UserIdCreation = p.UserIdCreation,
                        UserNameCreation = p.UserCreation.UserName != null ? p.UserCreation.UserName : "Unknown",
                        UserIdModification = p.UserIdModification != null ? p.UserCreation.UserName : "Unknown",
                        UserNameModification = p.UserModification.UserName != null ? p.UserModification.UserName : "Unknown" // Manejo de potencial null,

                    }).ToListAsync();
                    
                    return prod;
                }
                return null;
                

            }
            catch (Exception e)
            {

                Console.WriteLine("Date : " + DateTime.Now + " Error : " + e.Message);
                return null;
            }

        }
        public async Task<List<ResultProductionDto>> GetProductionsByDateDescending()
        {
            try
            {
                var prod = await _context.Productions
                    .Include(user => user.UserCreation)//incluir el username create
                    .Include(user => user.UserModification)//incluir el username modification
                    .OrderByDescending(x=>x.CreationTime)
                    .Select(p => new ResultProductionDto
                    {
                        Id = p.Id,
                        ProductName = this.encryptedSevice.UnProtect(p.ProductName),
                        Batch = this.encryptedSevice.UnProtect(p.Batch),
                        StoreId = p.StoreId,
                        Quantity = p.Quantity,
                        Tank = this.encryptedSevice.UnProtect(p.Tank),
                        FinalLevel = this.encryptedSevice.UnProtect(p.FinalLevel),
                        CreationTime = p.CreationTime,
                        ModificacionTime = p.ModificacionTime,
                        Comments = this.encryptedSevice.UnProtect(p.Comments),
                        UserIdCreation = this.encryptedSevice.UnProtect(p.UserIdCreation),
                        UserNameCreation = p.UserCreation.UserName != null ? p.UserCreation.UserName : "Unknown",
                        UserIdModification = p.UserIdModification != null ? p.UserCreation.UserName : "Unknown",
                        UserNameModification = p.UserModification.UserName != null ? p.UserModification.UserName : "Unknown" // Manejo de potencial null,

                    }).ToListAsync();
                // await _context.DisposeAsync();
                return prod;

            }
            catch (Exception e)
            {

                Console.WriteLine("Date : " + DateTime.Now + " Error : " + e.Message);
                return null;
            }

        }

        public async Task<List<ResultProductionDto>> GetProductionsByDate(DateTime? searchDate)
        {
            try
            {
                
               
                var prod = await _context.Productions
                    .Include(user => user.UserCreation)//incluir el username create
                    .Include(user => user.UserModification)//incluir el username modification
                    .Where(z => z.CreationTime.Date == searchDate.GetValueOrDefault().ToUniversalTime().Date) // Comparación por rango de fecha
                    .Select(p => new ResultProductionDto
                    {
                        Id = p.Id,
                        ProductName = this.encryptedSevice.UnProtect(p.ProductName),
                        Batch = this.encryptedSevice.UnProtect(p.Batch),
                        StoreId = p.StoreId,
                        Quantity = p.Quantity,
                        Tank = this.encryptedSevice.UnProtect(p.Tank),
                        FinalLevel = this.encryptedSevice.UnProtect(p.FinalLevel),
                        CreationTime = p.CreationTime,
                        ModificacionTime = p.ModificacionTime,
                        Comments = this.encryptedSevice.UnProtect(p.Comments),
                        UserIdCreation = this.encryptedSevice.UnProtect(p.UserIdCreation),
                        UserNameCreation = p.UserCreation.UserName != null ? p.UserCreation.UserName : "Unknown",
                        UserIdModification = p.UserIdModification != null ? p.UserCreation.UserName : "Unknown",
                        UserNameModification = p.UserModification.UserName != null ? p.UserModification.UserName : "Unknown" // Manejo de potencial null,

                    }).ToListAsync();
                // await _context.DisposeAsync();
                return prod;

            }
            catch (Exception e)
            {

                Console.WriteLine("Date : " + DateTime.Now + " Error : " + e.Message);
                return null;
            }

        }

        public async Task<List<ResultProductionDto>> GetProductionsByDateAscendin()
        {
            try
            {
                var prod = await _context.Productions
                    .Include(user => user.UserCreation)//incluir el username create
                    .Include(user => user.UserModification)//incluir el username modification
                    .OrderBy(x => x.CreationTime)
                    .Select(p => new ResultProductionDto
                    {
                        Id = p.Id,
                        ProductName = this.encryptedSevice.UnProtect(p.ProductName),
                        Batch = this.encryptedSevice.UnProtect(p.Batch),
                        StoreId = p.StoreId,
                        Quantity = p.Quantity,
                        Tank = this.encryptedSevice.UnProtect(p.Tank),
                        FinalLevel = this.encryptedSevice.UnProtect(p.FinalLevel),
                        CreationTime = p.CreationTime,
                        ModificacionTime = p.ModificacionTime,
                        Comments = this.encryptedSevice.UnProtect(p.Comments),
                        UserIdCreation = this.encryptedSevice.UnProtect(p.UserIdCreation),
                        UserNameCreation = p.UserCreation.UserName != null ? p.UserCreation.UserName : "Unknown",
                        UserIdModification = p.UserIdModification != null ? p.UserCreation.UserName : "Unknown",
                        UserNameModification = p.UserModification.UserName != null ? p.UserModification.UserName : "Unknown" // Manejo de potencial null,

                    }).ToListAsync();
                // await _context.DisposeAsync();
                return prod;

            }
            catch (Exception e)
            {

                Console.WriteLine("Date : " + DateTime.Now + " Error : " + e.Message);
                return null;
            }

        }

        public async Task<string> Create(ProductionDto dto, string secret)
        {
            // Usar transacciones asíncronas
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                string message = "0";
                var HashName = this.encryptedSevice.HashString(dto.ProductName);
                var HashBatch = this.encryptedSevice.HashString(dto.Batch);
                // Buscar si existe un producto con el mismo nombre y lote
                var store = await _context.Stores
                    .Where(x => x.ProductNameHash.Equals(HashName) &&
                            x.BatchHash.Equals(HashBatch))
                    .FirstOrDefaultAsync();

                if (store != null)
                {
                    Production production = new()
                    {
                        ProductNameHash = this.encryptedSevice.HashString(dto.ProductName),
                        ProductName = this.encryptedSevice.Protect(dto.ProductName),
                        BatchHash = this.encryptedSevice.HashString(dto.Batch),
                        Batch = this.encryptedSevice.Protect(dto.Batch),
                        Quantity = dto.Quantity,
                        Tank = this.encryptedSevice.Protect(dto.Tank),
                        FinalLevel = this.encryptedSevice.Protect(dto.FinalLevel),
                        Comments = this.encryptedSevice.Protect(dto.Comments),
                        StoreId = store.Id, // Obtener ID del record en Stores
                        CreationTime = DateTime.SpecifyKind(dto.CreationTime, DateTimeKind.Utc),
                        UserIdCreation = secret,
                        UserIdModification = null,
                        ModificacionTime = null
                    };

                    // Guardar el record de Production
                    await _context.Productions.AddAsync(production);


                    // Actualizar la cantidad en el Store
                    var quantity = store.ActualQuantity - dto.Quantity;
                    Console.WriteLine("quantity : " + quantity + " - " + dto.Quantity);
                    store.ActualQuantity = quantity;
                    _context.Stores.Update(store);

                    // Guardar cambios en la base de datos
                    await _context.SaveChangesAsync();

                    // Confirmar la transacción
                    await transaction.CommitAsync();
                    return "1";
                }

                // Si el lote o nombre es incorrecto, retornar mensaje
                return "Batch or product name is incorrect, try again!";
            }
            catch (Exception e)
            {
                // Rollback en caso de excepción
                await transaction.RollbackAsync();
                return e.Message;
            }
        }
    }

}       
            
           
       

