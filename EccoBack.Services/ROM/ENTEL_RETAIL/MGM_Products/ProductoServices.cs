using EccoBack.Abstraction;
using EccoBack.Repository;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Dto;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Mappers;
using AutoMapper;
using EccoBack.Repository.ROM.ENTEL_RETAIL.MGM_Products;

namespace EccoBack.Services.ROM.ENTEL_RETAIL.MGM_Products
{
    public class ProductoServices : IProductoServices
    {
        private readonly IProductoRepository _productRepository;

        private readonly IMapper _mapper;

        public ProductoServices(IProductoRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Task<Producto> Add(Producto entity)
        {
            return _productRepository.Add(entity);

        }

        public Task<Producto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Producto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(Producto entity)
        {
            throw new NotImplementedException();
        }

        public Task<Producto> Update(Producto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductoDTO>> ObtenerTodo()
        {
            var products = await _productRepository.ObtenerTodo();
            return products.Select(ProductMapper.MapToProductListDTO).ToList();

            //var productos = await _productRepository.ObtenerTodo();
            //return _mapper.Map<List<ProductoDTO>>(productos);
        }



        //public async Task<List<Producto>> ObtenerTodo()
        //{
        //    return await _productRepository.ObtenerTodo();
        //}



    }
}