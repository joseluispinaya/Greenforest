using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Respuesta<T>
    {
        public bool Estado { get; set; }
        public string Valor { get; set; }
        public T Data { get; set; }
    }
}
