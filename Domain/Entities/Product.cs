using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
   public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        [Required]
        [Range(0, 999.99, ErrorMessage = " Quantity can`t be less than 0 and more than {1}.")]
        public int Quantity { get; set; }

      
        [EnumDataType(typeof(Dimension))]
        public Dimension dimension { get; set; }

       

    }
    
    public enum Dimension
    {
        boxes,
        packages,
        pieces, 
        liters,
        milliliters,
        kilogram,
        grams
    }
   
}
