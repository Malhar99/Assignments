
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace MVC.Models
    
{
    public class ProductDetailMVC
    {
        [Required]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "This Field is Required Minimum 3 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Range(1, 10000)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Range(1, 10)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Maximum 100 characters")]
        public string Short_Description { get; set; }

        [StringLength(250, MinimumLength = 3, ErrorMessage = "Maximum 250 characters")]
        public string Long_Description { get; set; }

        [Required(ErrorMessage = "Please select Image file. (png|jpg|gif)")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.  (png/jpg/gif)")]
        public string Small_Image_Path { get; set; }

        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.  (png/jpg/gif)")]
        public string Large_Image_Path { get; set; }
    }
}