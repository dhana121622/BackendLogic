using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class UserLoginDbContext :DbContext
    {
        public UserLoginDbContext(DbContextOptions<UserLoginDbContext> dbContext) :base(dbContext) 
        {
            
        }

        
        public DbSet<Models.UserLogin> UserLoginApi { get; set; }

        public DbSet<Models.WorkUpdate> WorkUpdate { get; set; }

        public DbSet<Models.AdminLogin> AdminLogin { get; set; }

        public DbSet<Models.Group_Chat> GroupChat { get; set; }

        public DbSet<Models.GroupDetails> GroupDetails { get; set; }

    }
}
