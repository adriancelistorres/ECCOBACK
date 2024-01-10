using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EccoBack.Abstraction
{
    public interface IRepository<T>
    {
        IList<T> GetProducto();


    }
}
