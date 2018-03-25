using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace sportex.api.persistence
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DbSet;

        public Repository()
        {
        }

        public void Insert(T entity)
        {
            try
            {
                using (var dataContext = new Context())
                {
                    DbSet = dataContext.Set<T>();
                    DbSet.Add(entity);
                    dataContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexión con la base de datos:" + ex.Message);
            }
        }

        public void Delete(T entity)
        {
            try
            {
                using (var dataContext = new Context())
                {
                    DbSet = dataContext.Set<T>();
                    DbSet.Remove(entity);
                    dataContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexión con la base de datos:" + ex.Message);
            }
        }

        public List<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            try
            {
                using (var dataContext = new Context())
                {
                    DbSet = dataContext.Set<T>();
                    return DbSet.Where(predicate).ToList<T>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexión con la base de datos:" + ex.Message);
            }
        }
        //public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        //{
        //    try
        //    {
        //        using (var dataContext = new Context())
        //        {
        //            DbSet = dataContext.Set<T>();
        //            return DbSet.Where(predicate);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error en la conexión con la base de datos:" + ex.Message);
        //    }
        //}

        public List<T> GetAll()
        {
            try
            {
                using (var dataContext = new Context())
                {
                    DbSet = dataContext.Set<T>();
                    return DbSet.ToList<T>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexión con la base de datos:" + ex.Message);
            }
        }

        public T GetById(int id)
        {
            try
            {
                using (var dataContext = new Context())
                {
                    DbSet = dataContext.Set<T>();
                    return DbSet.Find(id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexión con la base de datos:" + ex.Message);
            }
        }

        public void Update(T entity)
        {
            try
            {
                using (var dataContext = new Context())
                {
                    var entry = dataContext.Entry(entity);
                    entry.State = EntityState.Modified;
                    dataContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexión con la base de datos:" + ex.Message);
            }
        }


    }
}
