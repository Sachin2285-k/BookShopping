using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Project_1.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        public string Author { get; set; }
        [Range(1, 10000)]
        [Required]
        public double ListPrice { get; set; } // 590
        [Range(1, 10000)]
        [Required]
        public double Price50 { get; set; } // 460
        [Range(1, 10000)]
        [Required]
        public double Price100 { get; set; } // 430
        [Range(1, 10000)]
        [Required]
        public double Price { get; set; } // 500
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Display(Name = "Cover Type")]
        public int CoverTypeId { get; set; }
        public CoverType CoverType { get; set; }

    }
}
