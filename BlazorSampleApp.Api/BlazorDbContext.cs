using BlazorSampleApp.Api.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorSampleApp.Api
{
    public class BlazorDbContext : DbContext
    {
        public BlazorDbContext(DbContextOptions<BlazorDbContext> options) : base(options)
        { }

        public DbSet<TodoDataModel> Todos { get; set; }
    }
}