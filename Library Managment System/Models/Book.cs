using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_Managment_System.Models
{
    public class Book
    {
        
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public int? Price { get; set; }

        [Required]
        [Display(Name = "Number In Stock")]
        public int? NumberInStock { get; set; }

        [Required]
        [Display(Name = "Number Available")]
        public int? NumberAvailable { get; set; }

        public Category Category { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

    }
}