using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IgualFabricante.Contracts;
using IgualFabricante.Logic.Entities;
using IgualFabricante.Logic.Entities.Persistence;

namespace IgualFabricante.Logic.DataContext.Db
{
    partial class DbIgualFabricanteContext : DbContext, IContext
    {
        private static string ConnectionString { get; set; } = "Data Source=(localdb)\\MSSQLLocalDb;Database=IgualFabricanteDB;Integrated Security=True;";

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bill>()
                .ToTable(nameof(Bill))
                .HasKey(p => p.id);
            modelBuilder.Entity<Bill>()
                .HasIndex(p => p.Title)
                .IsUnique();
            modelBuilder.Entity<Bill>()
                .Property(p => p.Description)
                .HasMaxLength(256);
            modelBuilder.Entity<Bill>()
                .Property(p => p.Friends)
                .IsRequired()
                .HasMaxLength(256);
            modelBuilder.Entity<Bill>()
                .Property(p => p.Currency)
                .IsRequired()
                .HasMaxLength(10);
            modelBuilder.Entity<Bill>()
                .Property(p => p.Title)
                .HasMaxLength(256);

            modelBuilder.Entity<Expense>()
                .ToTable(nameof(Expense))
                .HasKey(p => p.id);
            modelBuilder.Entity<Expense>()
                .Property(p => p.Designation)
                .IsRequired()
                .HasMaxLength(256);
            modelBuilder.Entity<Expense>()
                .Property(p => p.Friend)
                .IsRequired()
                .HasMaxLength(50);
        }
        #region IContext
        #region Async-Methods
        public Task<int> CountAsync<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, I
        {
            return Set<E>().CountAsync();
        }
        public Task<E> CreateAsync<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new()
        {
            return Task.Run(() => new E());
        }

        public Task<E> InsertAsync<I, E>(I entity)
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new()
        {
            return Task.Run(() =>
            {
                E newEntity = new E();

                newEntity.CopyProperties(entity);
                newEntity.id = 0;
                try
                {
                    if (Entry(newEntity).State == EntityState.Detached)
                    {
                        Entry(newEntity).State = EntityState.Added;
                    }
                }
                catch (Exception ex)
                {
                    Entry(newEntity).State = EntityState.Detached;
                    throw ex;
                }
                return newEntity;
            });
        }
        public Task<E> UpdateAsync<I, E>(I entity)
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new()
        {
            return Task.Run(() =>
            {
                var updEntity = new E();

                updEntity.CopyProperties(entity);

                var omEntity = Entry(updEntity);

                if (omEntity.State == EntityState.Detached)
                {
                    E attachedEntity = Set<E>().Local.SingleOrDefault(e => e.id == entity.id);

                    if (attachedEntity != null)
                    {
                        Entry(attachedEntity).CurrentValues.SetValues(entity);
                        Entry(attachedEntity).State = EntityState.Modified;
                    }
                    else
                    {
                        omEntity.State = EntityState.Modified;
                    }
                }
                else
                {
                    EntityState saveState = omEntity.State;

                    try
                    {
                        Entry(entity).State = EntityState.Modified;
                    }
                    catch
                    {
                        Entry(entity).State = saveState;
                        throw;
                    }
                }
                return omEntity.Entity;
            });
        }
        public Task<E> DeleteAsync<I, E>(int id)
            where I : IIdentifiable
            where E : IdentityObject, I
        {
            return Task.Run(() =>
            {
                E result = Set<E>().SingleOrDefault(i => i.id == id);

                if (result != null)
                {
                    Set<E>().Remove(result);
                }
                return result;
            });
        }

        public Task SaveAsync()
        {
            return SaveChangesAsync();
        }
        #endregion Async-Methods
        #endregion IContext
    }
}