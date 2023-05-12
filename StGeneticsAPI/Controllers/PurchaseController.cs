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

        /// <summary>
        /// Asynchronous method to insert a purchase into the system
        /// </summary>
        /// <returns></returns>

        [AcceptVerbs("POST")]
        public async Task<List<OrderTotalPurchaseModel>> PostPurchase(PurchasesModel Purchase)
        {
            var response = await ClPurchase.InsertPurchase(Purchase);
            return response;
        }
    }
}