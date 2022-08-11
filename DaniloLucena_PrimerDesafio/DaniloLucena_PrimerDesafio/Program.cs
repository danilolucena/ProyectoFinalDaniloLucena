using DaniloLucena_PrimerDesafio.ADO_.NET;

namespace DaniloLucena_PrimerDesafio;
public class Probar
{
    static void Main(string[] args)
    {
   
        UsuarioHandler usuarioHandler = new UsuarioHandler();

        Usuario usu = new Usuario();
        usu.Nombre = "Danilo";
        usu.Apellido = "Lucena";
        usu.NombreUsuario = "Disko10";
        usu.Contraseña = "Danilo10";
        usu.Mail = "daniloj.lucena@gmail.com";

        usuarioHandler.GetUsuariosPorNombre(usu.Nombre);
        usuarioHandler.InicioSesion(usu.NombreUsuario, usu.Contraseña);
        //usuarioHandler.InicioSesion("juan", "juan123");

        ProductoHandler productoHandler = new ProductoHandler();

        Producto pro = new Producto();
        pro.Id = 3;
        pro.Descripcion = "Bermuda";
        pro.Costo = 600;
        pro.PrecioVenta = 3000;
        pro.Stock = 10;
        pro.IdUsuario = 1;

        productoHandler.GetProductoXUsuario(pro.Id);

        VentasHandler ventasHandler = new VentasHandler();

        Venta v = new Venta();

        v.Id = 1;
        v.Comentarios = "";

        ventasHandler.GetVentasXId(v.Id);

    }
}
