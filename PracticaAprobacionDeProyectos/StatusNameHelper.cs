using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaAprobacionDeProyectos
{
    public static class StatusNameHelper
    {
        public static string GetStatusName(int status)
        {
            return status switch
            {
                1 => "Aprobado",
                2 => "Rechazado",
                3 => "Observado",
                _ => "Desconocido"
            };
        }
    }
}
