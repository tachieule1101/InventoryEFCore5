//Add data validation
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEFCore5.Models
{
    //Add data validation
    [ModelMetadataType(typeof(ProductsMetaData))]
    public partial class Products
    {

    }
    public class ProductsMetaData
    {
        //Đặt tên Thuộc tính
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Display(Name = "Available quantity")]
        public int AvailableQuantity { get; set; }
    }
    //End Add data validation
}
