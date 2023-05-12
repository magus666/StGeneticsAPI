using StGeneticsAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StGeneticsAPI.Logic
{
    public class Cl_Purchase
    {
        SqlConnection ConexionSqlServer = new SqlConnection(Properties.Settings.Default.StringConnection);
        SqlCommand Comando;
        SqlDataAdapter AdaptadorSql = new SqlDataAdapter();
        string RespuestaBaseDatos;

        public async Task<List<OrderTotalPurchaseModel>> InsertPurchase(PurchasesModel Purchase)
        {
            try
            {
                await ConexionSqlServer.OpenAsync();
                Comando = new SqlCommand("SPR_INS_PURCHASES", ConexionSqlServer);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddWithValue("@PAR_PURCHASESCODE", Purchase.PurchasesCode);
                Comando.Parameters.AddWithValue("@PAR_ANIMALID", Purchase.AnimalId);
                Comando.Parameters.AddWithValue("@PAR_ORDERSID", Purchase.OrdersId);

                using (var RespuestaReader = Comando.ExecuteReader())
                {
                    List<OrderTotalPurchaseModel> OrderTotalPurchase = new List<OrderTotalPurchaseModel>();
                    while (RespuestaReader.Read())
                    {

                        OrderTotalPurchaseModel ObjectOrderPurchase = new OrderTotalPurchaseModel();
                        ObjectOrderPurchase.OrderId = (int)RespuestaReader["OrderId"];
                        ObjectOrderPurchase.TotalPurchase = (decimal)RespuestaReader["TotalPurchase"];
                        OrderTotalPurchase.Add(ObjectOrderPurchase);

                    }
                    return OrderTotalPurchase;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ConexionSqlServer.Close();
            }
        }

    }
}