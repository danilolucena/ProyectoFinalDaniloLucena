using MiApiEntrega.Controllers.DTOS;
using MiApiEntrega.Model;
using MiApiEntrega.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MiApiEntrega.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet(Name = "GetProductoVendido")]
        public List<ProductoVendido> GetProductoVendidos()
        {
            return ProductoVendidoHandler.GetProductoVendidos();
        }
        [HttpDelete]
        public bool EliminarProductoVendido([FromBody] int id)
        {
            try
            {
                return ProductoVendidoHandler.EliminarProductoVendido(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPost]
        public bool CrearProductoVendido([FromBody] PostProductoVendido productoVendido)
        {
            try
            {
                return ProductoVendidoHandler.CrearProductoVendido(new ProductoVendido
                {
                    Stock = productoVendido.Stock,
                    IdProducto = productoVendido.IdProducto,
                    IdVenta = productoVendido.IdVenta
                 

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut]
        public bool ModificarProductoVendido([FromBody] PutProductoVendido productoVendido)
        {
            return ProductoVendidoHandler.ModificarProductoVendido(new ProductoVendido
            {
                Stock = productoVendido.Stock,
                IdProducto = productoVendido.IdProducto,
                IdVenta = productoVendido.IdVenta,
                Id = productoVendido.Id
            }) ;
        }
        [HttpGet("/GetProductoVendidoXUsuario")]
        public List<ProductoVendido> GetProductoVendidoXUsuario([FromBody] int id)
        {

            return ProductoVendidoHandler.GetProductoVendidoXUsuario(id);
        }
    }
}
