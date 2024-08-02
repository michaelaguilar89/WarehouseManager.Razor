using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WareHouseManager.Razor.Models
{
    public class Production
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]

        public string ProductName { get; set; }
        [Required, MaxLength(30)]
        public string Batch { get; set; }

        [Required]
        public int StoreId { get; set; }
        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; }
        [Required, MaxLength(10)]
        public string Tank { get; set; }
        [Required, MaxLength(10)]
        public string FinalLevel { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        
        public DateTime? ModificacionTime { get; set; }
        [Required]
        public string UserIdCreation { get; set; }
        
        public string? UserIdModification { get; set; }
        [MaxLength(150)]
        public string Comments { get; set; }
        //navigation property
        [ForeignKey("UserIdCreation")]
        public ApplicationUser UserCreation { get; set; }

        [ForeignKey("UserIdModification")]
        public ApplicationUser UserModification { get; set; }
        [ForeignKey("StoreId")]
        public Store store { get; set; }


    }
}
