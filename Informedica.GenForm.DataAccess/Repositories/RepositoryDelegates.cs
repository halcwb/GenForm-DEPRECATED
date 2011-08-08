using System;
using System.Collections.Generic;
using Informedica.GenForm.Database;

namespace Informedica.GenForm.DataAccess.Repositories
{
    /// <summary>
    /// Base class to be implemented by a service class that provides 
    /// methods to persist Business Objects using Data Access Objects
    /// </summary>
    /// <typeparam name="T">Type of Business Object</typeparam>
    /// <typeparam name="TC">Type of Data Access Object</typeparam>
    public abstract class RepositoryDelegates<T, TC>
    {
        protected static RepositoryDelegates<T, TC> _instance;
        protected static readonly Object LockThis = new Object();

        /// <summary>
        /// Method to insert a data access object into the datacontext
        /// </summary>
        /// <param name="context">Data context</param>
        /// <param name="dao">Data Access Object</param>
        protected abstract void InsertDelegate(GenFormDataContext context, TC dao);

        /// <summary>
        /// Use the selector function on the data context to fetch a 
        /// list of Data Access Objects
        /// </summary>
        /// <param name="context">Data context</param>
        /// <param name="selector">Selector function</param>
        /// <returns></returns>
        protected abstract IEnumerable<TC> FetchDelegate(GenFormDataContext context, Func<TC, bool> selector);

        /// <summary>
        /// Use the selector function on the context to delete 
        /// </summary>
        /// <param name="context">Data Context</param>
        /// <param name="selector">Selector Function</param>
        protected abstract void DeleteDelegate(GenFormDataContext context, Func<TC, Boolean> selector);

        /// <summary>
        /// Get a selector function to select a Data Access Object according to Id
        /// of Business Object to retrieve
        /// </summary>
        /// <param name="id">Id of Business Object</param>
        /// <returns>Selector Function</returns>
        protected abstract Func<TC, Boolean> GetIdSelectorDelegate(Int32 id);

        /// <summary>
        /// Get a selector function to select Data Access Object's with name 
        /// of Business Object to retrieve
        /// </summary>
        /// <param name="name">Name of Business Object</param>
        /// <returns>Selector Function</returns>
        protected abstract Func<TC, Boolean> GetNameSelectorDelegate(String name);

    }
}
