using Gelir_Gider_Takibi.EnTiTy;

namespace Gelir_Gider_Takibi.Models
{
    public class ThismonThsalary_spendingModels
    {
        public ThisMonThSalary ThismonThsalary { get; set; }
        public List<ThisMonThSalarySpending> ThismonThsalaryspending { get; set; }
        public List<CaTegory> caTegoryspending { get; set; }
    }
}
