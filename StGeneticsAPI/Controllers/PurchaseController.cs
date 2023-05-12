using StGeneticsAPI.Logic;
using StGeneticsAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace StGeneticsAPI.Controllers
{
    public class PurchaseController : ApiController
    {
        Cl_Purchase ClPurchase = new Cl_Purchase();

        [AcceptVerbs("POST")]
        public async Task<List<OrderTotalPurchaseModel>> PostPurchase(PurchasesModel Purchase)
        {
            var response = await ClPurchase.InsertPurchase(Purchase);
            return response;
        }
    }
}