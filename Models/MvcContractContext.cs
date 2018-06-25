using Microsoft.EntityFrameworkCore;

namespace MvcContract.Models
{
    public class MvcContractContext : DbContext
    {
        public MvcContractContext(DbContextOptions<MvcContractContext> options)
            : base(options)
        {
        }

        public DbSet<MvcContract.Models.Contract> Contract { get; set; }
    }
}
