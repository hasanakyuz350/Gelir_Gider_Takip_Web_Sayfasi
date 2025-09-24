using Gelir_Gider_Takibi.EnTiTy;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ThisMonThSalarySpending
{
    [Key]
    public int salaryspendingID { get; set; }
    public decimal amounT { get; set; }
    //1-N
    public int monThID { get; set; }
    [ForeignKey("monThID")]
    public ThisMonThSalary ThisMonThSalary { get; set; }
    //1-N
    public int caTegoryID { get; set; }
    [ForeignKey("caTegoryID")]
    public CaTegory caTegory { get; set; }
}

