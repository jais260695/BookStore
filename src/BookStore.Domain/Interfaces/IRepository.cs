using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    //The class that will implement this interface, also need to implement the interface IDisposible for release memory.
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        //The Add method it’s for adding a new entity.
        Task Add(TEntity entity);

        //The GetAll method it’s to get all records for the entity.
        Task<List<TEntity>> GetAll();

        //The GetById method it’s to get an entity by it’s Id.
        Task<TEntity> GetById(int id);

        //The Update method it’s to update an entity.
        Task Update(TEntity entity);

        //The Remove method it’s to delete an entity.
        Task Remove(TEntity entity);

        //The Search method it’s for searching an entity passing a lambda expression to search any entity with any parameter.
        //This predicate it’s an expression, so you can use a lambda expression to filter objects,
        //it’s exactly the same way we use the where with linq.
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity,bool>> predicate);

        //The SaveChanges it’s for save the changes of the entity.
        //It will return an int, that will be the number of lines that were affected by the save action.
        Task<int> SaveChanges();
    }
}
