using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaniloLucena_PrimerDesafio
{
    public class Venta
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }

        public Venta()
        {
            Id = 0;
            Comentarios = string.Empty;
        }
        public Venta(int id, string comentarios)
        {
            Id = id;
            Comentarios = comentarios;
        }
    }
}
