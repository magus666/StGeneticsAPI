using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StGeneticsAPI.Models
{
    public class PurchasesModel
    {
        public int PurchasesId { get; set; }
        public string PurchasesCode { get; set; }
        public int AnimalId { get; set; }
        public int OrdersId { get; set; }
    }
}