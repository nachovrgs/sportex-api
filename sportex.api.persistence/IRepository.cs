﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace sportex.api.persistence
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(T entity);
        List<T> SearchFor(Expression<Func<T, bool>> predicate);
        //IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        List<T> GetAll();
        T GetById(int id);
        void Update(T entity);
    }
}
