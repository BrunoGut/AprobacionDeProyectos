using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Helpers
{
    public static class ApprovalStatus
    {
        public const int Pendiente = 1;
        public const int Aprobado = 2;
        public const int Rechazado = 3;
        public const int Observado = 4;

        public static string ToText(int status) => status switch
        {
            Pendiente => "Pendiente",
            Aprobado => "Aprobado",
            Rechazado => "Rechazado",
            Observado => "Observado",
            _ => "Desconocido"
        };
    }
}
