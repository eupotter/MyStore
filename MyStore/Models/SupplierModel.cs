using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class SupplierModel
    {
        [Required]
        public int Supplierid { get; set; }
        [Required]
        [MinLength(4)]
        public string Companyname { get; set; }
        public string Contactname { get; set; }
        public string Contacttitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postalcode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
