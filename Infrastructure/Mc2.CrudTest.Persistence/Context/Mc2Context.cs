using Mc2.CrudTest.Application.Interfaces;
using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using NetDevPack.Messaging;


namespace Mc2.CrudTest.Persistence.Context
{
    public class Mc2Context : DbContext, IMc2DbContext
    {
        #region Ctor
        public Mc2Context(DbContextOptions<Mc2Context> options) : base(options: options)
        {
           
        }
        #endregion Ctor

        #region DbSets

        public DbSet<Customer> Customers { get; set; }

        #endregion DbSets

        #region Overrided methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        #endregion Overrided methods
    }
}