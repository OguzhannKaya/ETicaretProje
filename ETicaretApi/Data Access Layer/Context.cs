using Microsoft.EntityFrameworkCore;

namespace ETicaretApi.Data_Access_Layer
{
    public class Context: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=OGKAYA\MSSQLSERVER1;Database=ETicaretProjeApi; User Id=sa; Password=1234567890Aa;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<UserApi> UsersApi {  get; set; }  
    }
}
