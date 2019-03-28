using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SktProject.Models.ViewModel
{
    public class ShoppingCartViewModels
    {
      
        public decimal CartTotal { get; set; }
        public List<Cart> CartItems { get; set; }

    }
}