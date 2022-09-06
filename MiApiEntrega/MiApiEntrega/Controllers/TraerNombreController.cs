using MiApiEntrega.Controllers.DTOS;
using MiApiEntrega.Model;
using MiApiEntrega.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MiApiEntrega.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TraerNombreController : ControllerBase
    {
        [HttpGet(Name = "TraerNombre")]
        public string TraerNombre()
        {
            return "Sistema de Gestión";
        }
    }
}
