using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WareHouseManager.Razor.Dto_s
{
    public class StoreDto
    {
        [Required(ErrorMessage = "Product Name is required."),MinLength(3), MaxLength(30)]

        public string ProductName { get; set; }
        [Required(ErrorMessage = "Batch is required."),MinLength(3), MaxLength(30)]
        public string Batch { get; set; }
        [Required(ErrorMessage = "Quantity is required."), Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; }
        [Required(ErrorMessage = "Creation Time is required.")]
        public DateTime CreationTime { get; set; }

        
        [Required(ErrorMessage ="Description is required"), MaxLength(150)]
        public string Description { get; set; }
        [Required(ErrorMessage="Description is required"), MaxLength(150)]
        public string Comments { get; set; }
        
      

        

    }
}
