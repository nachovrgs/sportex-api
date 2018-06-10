using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace sportex.api.persistance.Extensions
{
    public static class RepositoryExtensions
    {
        //Extensions
        /// <summary>
        /// Includes an array of navigation properties for the specified query 
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="query">The query to include navigation properties for that</param>
        /// <param name="navProperties">The array of navigation properties to include</param>
        /// <returns></returns>
        public static IQueryable<T> CustomInclude<T>(this IQueryable<T> query, params string[] navProperties)
            where T : class
        {
            foreach (var navProperty in navProperties)
                query = query.Include(navProperty);

            return query;
        }
    }
}
