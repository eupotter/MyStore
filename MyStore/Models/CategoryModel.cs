using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class CategoryModel
    {
        [Required]
        public int Categoryid { get; set; }
        [Required]
        [MinLength(4)]
        public string Categoryname { get; set; }
        public string Description { get; set; }
    }
}
