using Microsoft.EntityFrameworkCore;
using Operacao_curiosidade_API.Models;

namespace Operacao_curiosidade_API.Persistence
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}