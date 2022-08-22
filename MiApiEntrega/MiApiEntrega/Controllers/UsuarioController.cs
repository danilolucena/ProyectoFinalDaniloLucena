﻿using MiApiEntrega.Controllers.DTOS;
using MiApiEntrega.Model;
using MiApiEntrega.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MiApiEntrega.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "GetUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            return UsuarioHandler.GetUsuarios();
        }
        [HttpDelete]
        public bool EliminarUsuario([FromBody] int id)
        {
            try
            {
                return UsuarioHandler.EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut]
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.ModificarNombreDeUsuario(new Usuario
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre
            });
        }

        [HttpPost]
        public bool CrearUsuario([FromBody] PostUsuario usuario)
        {
            try
            {
                return UsuarioHandler.CrearUsuario(new Usuario
                {
                    Apellido = usuario.Apellido,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail,
                    Nombre = usuario.Nombre,
                    NombreUsuario = usuario.NombreUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [HttpGet]
        public Usuario GetUsuariosPorNombre([FromBody] string nombreUsuario)
        {
            return UsuarioHandler.GetUsuariosPorNombre(nombreUsuario);
        }

        [HttpGet]
        public Usuario InicioSesion([FromBody] string nombre, string contraseña)
        {
            return UsuarioHandler.InicioSesion(nombre,contraseña);
        }

    }
}