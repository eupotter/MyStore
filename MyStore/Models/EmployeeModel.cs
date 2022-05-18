using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class EmployeeModel
    {
        [Required]
        public int Empid { get; set; }
        [Required]
        [MinLength(3)]
        public string Lastname { get; set; }
        [Required]
        [MinLength(3)]
        public string Firstname { get; set; }
        public string Title { get; set; }
        public string Titleofcourtesy { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Hiredate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postalcode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public int? Mgrid { get; set; }
    }
}
