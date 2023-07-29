using System;
using System.Collections.Generic;

#nullable disable

namespace First_Project.Models
{
    public partial class Orderf
    {
        public Orderf()
        {
            ProductOrderves = new HashSet<ProductOrderf>();
            Reports = new HashSet<Report>();
        }

        public decimal Id { get; set; }
        public DateTime? DateOrder { get; set; }
        public decimal? AccId { get; set; }

        public virtual Useraccf Acc { get; set; }
        public virtual ICollection<ProductOrderf> ProductOrderves { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
