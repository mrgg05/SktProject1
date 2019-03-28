using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SktProject.Models.ViewModel
{
    public class IndexViewModels
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
       
        public decimal Price { get; set; }
        public string ProductUrl { get; set; }
        public DateTime SKT { get; set; }

    }
}