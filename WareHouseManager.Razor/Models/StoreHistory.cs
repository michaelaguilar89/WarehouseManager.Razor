using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WareHouseManager.Razor.Models
{
    public class StoreHistory
    {

        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string ProductName { get; set; }
        [Required, MaxLength(30)]
        public string Batch { get; set; }
        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }

        public DateTime ModificacionTime { get; set; }
        [Required]
        public string Action { get; set; }
        [Required]
        public string UserIdCreation { get; set; }

        [MaxLength(150)]
        public string Comments { get; set; }
        public string UserIdModification { get; set; }
        [ForeignKey("UserIdCreation")]
        public ApplicationUser UserCreation { get; set; }
        [ForeignKey("UserIdModification")]
        public ApplicationUser UserModification { get; set; }

    }
}
