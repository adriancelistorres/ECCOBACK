using EccoBack.Abstraction;
using EccoBack.Entities;

namespace Ecco.Services
{
    public class Services<T>:IServicesProd<Producto>
    {
        private readonly IRepository<Producto> _productRepository;

        public Services(IRepository<Producto> productRepository)
        {
            _productRepository = productRepository;
        }

        public IList<Producto> GetProducto()
        {
           return _productRepository.GetProducto(); 
        }
    }
}