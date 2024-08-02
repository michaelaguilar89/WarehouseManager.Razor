
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WareHouseManager.Razor.Dto_s
{
    public class ResultStoreDto
    {
        
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Batch { get; set; }
        [Required]
        public decimal TotalQuantity { get; set; }
        [Required]
        public decimal ActualQuantity { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }
        public DateTime? ModificationAt { get; set; }
        
        public string? Description { get; set; }
        
        public string? Comments { get; set; }
        [Required]
        public string? UserIdCreation { get; set; }
        public string? UserNameCreation { get; set; }

        public string? UserIdModification { get; set; }
        public string? UserNameModification { get; set; }
       

    }
}
