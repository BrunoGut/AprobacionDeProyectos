using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Helpers
{
    public static class ApprovalStatusHelper
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

        public static int FromString(string status)
        {
            return status.ToLower() switch
            {
                "pendiente" => Pendiente,
                "aprobado" => Aprobado,
                "rechazado" => Rechazado,
                _ => throw new ArgumentException($"Estado desconocido: {status}")
            };
        }

        private static readonly HashSet<string> ValidStatuses = new() { "Pendiente", "Aprobado", "Rechazado" };

        public static bool IsValidStatus(string status) => ValidStatuses.Contains(status);
    }
}
