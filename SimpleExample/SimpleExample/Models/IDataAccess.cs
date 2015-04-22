using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExample.Models
{
    public interface IDataAccess<T>
    {
        List<T> GetAll { get; }
        void Create(T instance);

        bool Update(T instance);

        void Delete(T instance);

        List<T> Find(string item);
    }
}
