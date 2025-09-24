using Gelir_Gider_Takibi.EnTiTy;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ThisMonThSalary
{
    [Key]
    public int monThID { get; set; }
    public string monThname { get; set; }
    public int year { get; set; }
    public decimal salary { get; set; }
    //1-N
    public int userID { get; set; }
    [ForeignKey("userID")]
    public User User { get; set; }
    //1-N
    public List<ThisMonThSalarySpending> ThismonThsalaryspending { get; set; }
}

