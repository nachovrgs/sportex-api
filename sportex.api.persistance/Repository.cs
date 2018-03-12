using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace UserAPI.DBAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DbSet;

        public Repository()
        {
        }

        #region IRepository<T> Members

        public void Insert(T entity)
        {
            try
            {
                using (var dataContext = new UserContext())
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
                using (var dataContext = new UserContext())
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

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            try
            {
                using (var dataContext = new UserContext())
                {
                    DbSet = dataContext.Set<T>();
                    return DbSet.Where(predicate);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexión con la base de datos:" + ex.Message);
            }
        }

        public List<T> GetAll()
        {
            try
            {
                using (var dataContext = new UserContext())
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
                using (var dataContext = new UserContext())
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

        #endregion

        //VIEJO

        //public Repository(DbContext dataContext)
        //{
        //    DbSet = dataContext.Set<T>();
        //}

        //#region IRepository<T> Members

        //public void Insert(T entity)
        //{
        //    try
        //    {
        //        DbSet.Add(entity);
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Error en la conexión con la base de datos.");
        //    }
        //}

        //public void Delete(T entity)
        //{
        //    try
        //    {
        //        DbSet.Remove(entity);
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Error en la conexión con la base de datos.");
        //    }
        //}

        //public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        //{
        //    try
        //    {
        //        return DbSet.Where(predicate);
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Error en la conexión con la base de datos.");
        //    }
        //}

        //public IQueryable<T> GetAll()
        //{
        //    try
        //    {
        //        return DbSet;
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Error en la conexión con la base de datos.");
        //    }
        //}

        //public T GetById(int id)
        //{
        //    try
        //    {
        //        return DbSet.Find(id);
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Error en la conexión con la base de datos.");
        //    }
        //}

        //#endregion
    }
}
