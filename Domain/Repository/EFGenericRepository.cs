using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Domain.Repository
{
    //Generic Repository that implement CRUD operations
    public class EFGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        LibraryContext context;
        DbSet<TEntity> genericEntity;

        public EFGenericRepository(LibraryContext _context)
        {
            this.context = _context;
            genericEntity = context.Set<TEntity>();
        }


        public void Create(TEntity item)
        {
            genericEntity.Add(item);
           // context.SaveChanges();
        }

        public void Delete(int? id)
        {
            var item = genericEntity.Find(id.Value);
            genericEntity.Remove(item);
           // context.SaveChanges();
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return context.Set<TEntity>().Where(predicate).ToList();
        }

        public TEntity Get(int id)
        {
            return genericEntity.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return genericEntity.ToList();
        }

        public void Update(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
          //  context.SaveChanges();
        }
        
 
    }

}
