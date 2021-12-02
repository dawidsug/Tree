using System;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Leaf> Leafs { get; set; }
        public DbSet<TreeNode> Nodes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Leaf>()
                .HasOne(l => l.Parent)
                .WithMany(n => n.Leafs);
        }
    }
}