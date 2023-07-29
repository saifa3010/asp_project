using System;
using System.Collections.Generic;

#nullable disable

namespace First_Project.Models
{
    public partial class ProductOrderf
    {
        public ProductOrderf()
        {
            Paymantfs = new HashSet<Paymantf>();
        }

        public decimal Id { get; set; }
        public string Status { get; set; }
        public decimal? ProductId { get; set; }
        public decimal? OrderId { get; set; }
        public decimal? Quantity { get; set; }

        public virtual Orderf Order { get; set; }
        public virtual Productf Product { get; set; }
        public virtual ICollection<Paymantf> Paymantfs { get; set; }
    }
}
