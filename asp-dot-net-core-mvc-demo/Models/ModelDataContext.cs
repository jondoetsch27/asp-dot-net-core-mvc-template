using System;
using Microsoft.EntityFrameworkCore;

namespace asp.net.core.mvc.demo.Models
{
    public class ModelDataContext : DbContext
    {
        public DbSet<MLModel> MLModels { get; set; }

        public ModelDataContext(DbContextOptions<ModelDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
