using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Models.AStatistics
{
    public class AStatisticsModel
    {
        public int TotalCustomer { get; set; }
        public int TotalManager { get; set; }
        public int TotalPetInStore { get; set; }
        public int TotalPetQuatityInStore { get; set; }
        public int TotalPetQuantitySold { get; set; }
        public double TotalMoneyPetInStore { get; set; }
        public double TotalAmountSold { get; set; }

        public List<AStatisticsMonthSaleModel> AStatisticsMonthSaleModels { get; set; }
    }
}
