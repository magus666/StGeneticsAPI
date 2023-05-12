using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StGeneticsAPI.Models
{
    public class OrderTotalPurchaseModel
    {
        public int OrderId { get; set; }
        public decimal TotalPurchase { get; set; }

    }
}