using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace First_Project.Models
{
    public partial class Categoryf
    {
        public Categoryf()
        {
            Productfs = new HashSet<Productf>();
            Reports = new HashSet<Report>();
        }

        public decimal Id { get; set; }
        public string CategoryName { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public virtual ICollection<Productf> Productfs { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
