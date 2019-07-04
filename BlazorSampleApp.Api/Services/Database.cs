using System.Threading.Tasks;

namespace BlazorSampleApp.Api.Services
{
    public class Database
    {
        private readonly BlazorDbContext _context;

        public Database(BlazorDbContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}