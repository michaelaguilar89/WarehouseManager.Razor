using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WareHouseManager.Razor.Dto_s
{
    public class ProductionDto
    {
        [Required, MaxLength(50)]

        public string ProductName { get; set; }
        [Required, MaxLength(30)]
        public string Batch { get; set; }

       
        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; }
        [Required, MaxLength(10)]
        public string Tank { get; set; }
        [Required, MaxLength(10)]
        public string FinalLevel { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        public string Comments { get; set; }

    }
}
