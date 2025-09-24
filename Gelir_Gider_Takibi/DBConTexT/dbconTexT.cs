using Gelir_Gider_Takibi.EnTiTy;
using Microsoft.EntityFrameworkCore;

namespace Gelir_Gider_Takibi.DBConTexT
{
    public class dbconTexT : DbContext
    {
        public dbconTexT(DbContextOptions<dbconTexT> baglaTi) : base(baglaTi)
        {

        }
        public DbSet<User> user { get; set; }
        public DbSet<ThisMonThSalary> ThismonThsalary { get; set; }
        public DbSet<ThisMonThSalarySpending> ThismonThsalaryspending { get; set; }
        public DbSet<CaTegory> caTegory { get; set; }
    }
}
