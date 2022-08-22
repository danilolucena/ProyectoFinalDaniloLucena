using MiApiEntrega.Controllers.DTOS;
using MiApiEntrega.Model;
using MiApiEntrega.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MiApiEntrega.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet(Name = "GetVentas")]

        public List<Venta> GetVentas()
        {
            return VentaHandler.GetVentas();
        }
        [HttpDelete]
        public bool EliminarVenta([FromBody] int id)
        {
            try
            {
                return VentaHandler.EliminarVenta(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [HttpPost]
        public bool CrearVenta([FromBody] PostVenta venta)
        {
            try
            {
                return VentaHandler.CrearVenta(new Venta
                {
                   Comentarios = venta.Comentarios

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [HttpPut]
        public bool ModificarVenta([FromBody] PutVenta venta)
        {
            return VentaHandler.ModificarVenta(new Venta
            {
                Comentarios = venta.Comentarios,
                Id = venta.Id
            });
        }

        //[HttpGet]

        //public List<Venta> GetVentasXId([FromBody] int id)
        //{
        //    try
        //    {
        //        return VentaHandler.GetVentasXId(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return VentaHandler.GetVentasXId(id);
        //    }
        //}

    }
}
