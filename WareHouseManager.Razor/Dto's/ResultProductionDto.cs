
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WareHouseManager.Razor.Dto_s
{
    public class ResultProductionDto
    {
      
        public int Id { get; set; }
        

        public string ProductName { get; set; }
       
        public string Batch { get; set; }

        
        public int StoreId { get; set; }
        [ Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; }
        
        public string Tank { get; set; }
        
        public string FinalLevel { get; set; }
       
        public DateTime CreationTime { get; set; }

        public DateTime? ModificacionTime { get; set; }
        public string UserIdCreation { get; set; }
        public string UserNameCreation { get; set; }

        public string? UserIdModification { get; set; }
        public string? UserNameModification { get; set; }
        public string Comments { get; set; }
        //navigation property
        
    }
}
