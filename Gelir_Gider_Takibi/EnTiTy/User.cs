using System.ComponentModel.DataAnnotations;

namespace Gelir_Gider_Takibi.EnTiTy
{
    public class User
    {
        [Key]
        public int userID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        //1-N
        public List<ThisMonThSalary> ThismonThsalary { get; set; }
    }
}
