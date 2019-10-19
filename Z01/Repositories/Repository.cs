using System;
using System.Collections;


namespace Z01.Repositories
{
    public interface IRepository<T, ID> 
    {
        T FindById(ID id);
        IEnumerable FindAll();

        void Update(T obj);
    }
}