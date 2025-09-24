using System.ComponentModel.DataAnnotations;

namespace Gelir_Gider_Takibi.EnTiTy
{
    public class CaTegory
    {
        [Key]
        public int caTegoryID { get; set; }
        public string caTegoryname { get; set; }
        //1-N
        public List<ThisMonThSalarySpending> ThismonThsalaryspending { get; set; }
    }
}
