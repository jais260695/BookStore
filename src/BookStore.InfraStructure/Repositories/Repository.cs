using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.InfraStructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.InfraStructure.Repositories
{
    //This is an abstract class, which means that this class cannot be instantiated, it can only be inherited.
    //All specific repositories classes that we are going to create, will inherit from this main class.
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        //The Db property is protected because all classes that will inheritance from Repository, can access the Db property.
        protected readonly BookStoreDbContext Db;

        //The property DbSet is used as a shortcut to execute the operations in the database.
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(BookStoreDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        //we are using the AsNoTracking() because when we add something in the memory, the object will be tracking,
        //so in this case that we are only reading something in the database, we can use AsNoTracking to increase performance in our application.
        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        //There some methods that are virtual, and the reason for that it’s because we want to allow to do an override in other specific repository class if necessary.
        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
