using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace First_Project.Models
{
    public partial class Productf
    {
        public Productf()
        {
            ProductOrderves = new HashSet<ProductOrderf>();
            Reports = new HashSet<Report>();
        }

        public decimal Id { get; set; }
        public string ProductfName { get; set; }
        public string Imagepath { get; set; }
        public decimal? Price { get; set; }
        public decimal? Sale { get; set; }
        public string Description { get; set; }
        public decimal? CategoryId { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public virtual Categoryf Category { get; set; }
        public virtual ICollection<ProductOrderf> ProductOrderves { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
