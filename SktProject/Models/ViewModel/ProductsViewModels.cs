using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SktProject.Models.ViewModel
{
    public class ProductsViewModels
    {

        public int ProductId { get; set; }

        public string CategoryId { get; set; }
       

        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ProductUrl { get; set; }

        public DateTime SKT { get; set; }

        public DateTime TETT { get; set; }

        public DateTime ProductionDate { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}