﻿using System.Collections.Generic;
using System.Threading.Tasks;

using Interfaces.Status;

namespace Interfaces.Data
{
    public interface ILoadDelegate
    {
        Task LoadAsync();
    }

    public interface ISaveDelegate
    {
        Task SaveAsync();
    }

    public interface IGetByIdDelegate<Type>
    {
        Task<Type> GetByIdAsync(long id);
    }

    public interface IEnumerateIdsDelegate
    {
        IEnumerable<long> EnumerateIds();
    }

    public interface IUpdateDelegate<Type>
    {
        Task UpdateAsync(IStatus status, params Type[] data);
    }

    //public interface IAddDelegate<Type>
    //{
    //    Task AddAsync(Istatus status, params Type[] data);
    //}

    //public interface IModifyDelegate<Type>
    //{
    //    Task ModifyAsync(Istatus status, params Type[] data);
    //}

    public interface IRemoveDelegate<Type>
    {
        Task RemoveAsync(IStatus status, params Type[] data);
    }

    public interface IContainsDelegate<Type>
    {
        bool Contains(Type data);
    }

    public interface IContainsIdDelegate
    {
        bool ContainsId(long id);
    }

    public interface ICountDelegate
    {
        int Count();
    }

    public interface IDataController<Type>:
        ILoadDelegate,
        ISaveDelegate,
        IEnumerateIdsDelegate,
        ICountDelegate,
        IGetByIdDelegate<Type>,
        IUpdateDelegate<Type>,
        IRemoveDelegate<Type>,
        IContainsDelegate<Type>,
        IContainsIdDelegate
    {
        // ...
    }
}