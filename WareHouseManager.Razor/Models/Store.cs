using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WareHouseManager.Razor.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductNameHash { get; set; } // Campo adicional para el hash del nombre
        [Required]
        public string Batch { get; set; }
        [Required]
        public string BatchHash { get; set; } // Campo adicional para el hash del nombre

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal TotalQuantity { get; set; }
        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal ActualQuantity { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        
         
        [Required]
        public string Description { get; set; }
        [Required]
        public string Comments { get; set; }
        public DateTime? ModificationAt { get; set; }
        [Required]
        public string UserIdCreation { get; set; }
        
        public string? UserIdModification { get; set; }
        //navigation property
        [ForeignKey("UserIdCreation")]
        public ApplicationUser UserCreation { get; set; }

        [ForeignKey("UserIdModification")]
        public ApplicationUser UserModification { get; set; }

    }
}
