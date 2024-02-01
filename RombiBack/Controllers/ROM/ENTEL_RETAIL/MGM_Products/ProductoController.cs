using RombiBack.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Products;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Dto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServices _productoService;
        public ProductoController(IProductoServices productoService)
        {
            _productoService = productoService;
        }

        // Define un endpoint para obtener productos
        //[HttpGet]
        //public ActionResult GetProductos()
        //{
        //    try
        //    {
        //        // Llama al método en tu servicio para obtener la lista de productos
        //        var productos = _productoService.GetProducto();
        //        return Ok(productos);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de errores, puedes personalizar esto según tus necesidades
        //        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var productos = await _productoService.ObtenerTodo();
            return Ok(productos);
        }


        [HttpPost]
        public async Task<IActionResult> AgregarProducto([FromBody] Producto product)
        {
            try
            {
                // Aquí puedes utilizar AutoMapper para mapear tu DTO a la entidad del modelo de dominio
                // Supongo que tu DTO se llama ProductoDTO y es diferente de tu entidad Producto
                //Producto producto = _mapper.Map<Producto>(productoDTO);

                // Llamada al servicio para agregar el producto
                Producto productoAgregado = await _productoService.Add(product);

                // Puedes devolver el producto agregado si es necesario
                // También puedes devolver un código 201 Created con la ubicación del nuevo recurso, por ejemplo:
                // return CreatedAtAction(nameof(AgregarProducto), new { id = productoAgregado.intModeloEquipoID }, productoAgregado);

                return Ok(productoAgregado); // Devuelve el producto agregado como OK (200 OK)
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

    }
}
