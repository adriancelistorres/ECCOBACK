using EccoBack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecco.Services
{
    public interface IServicesProd<Producto>
    {
        IList<Producto> GetProducto();
    }
}
