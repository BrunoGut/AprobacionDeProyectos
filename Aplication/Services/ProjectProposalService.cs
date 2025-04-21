using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using Aplication.Interfaces;
//using PracticaAprobacionDeProyectos;
using Application.Interfaces;
using Aplication.Dtos;


namespace Aplication.Services
{
    public class ProjectProposalService : IProjectProposalService
    {
        private readonly IProjectProposalCommand _command;
        private readonly IUserQuery _userQuery;
        private readonly IAreaQuery _areaQuery;
        private readonly IProjectTypeQuery _typeQuery;

        public ProjectProposalService(IProjectProposalCommand command, IUserQuery userQuery, IAreaQuery areaQuery, IProjectTypeQuery typeQuery)
        {
            _command = command;
            _userQuery = userQuery;
            _areaQuery = areaQuery;
            _typeQuery = typeQuery;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userQuery.GetAllUsers();
        }

        public async Task<List<Area>> GetAllAreasAsync()
        {
            return await _areaQuery.GetAllAreas();
        }

        public async Task<List<ProjectType>> GetAllTypesAsync()
        {
            return await _typeQuery.GetAllTypes();
        }

        public async Task CreateRequestAsync()
        {
            Console.Clear();
            Console.WriteLine("=== CREAR SOLICITUD DE PROYECTO ===");

            // Mostrar usuarios
            var users = await _userQuery.GetAllUsers();
            Console.WriteLine("Usuarios disponibles:");
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id} - Nombre: {user.Name}");
            }

            int userId;
            while (true)
            {
                Console.Write("Ingrese el ID del usuario que crea la solicitud: ");
                if (int.TryParse(Console.ReadLine(), out userId) && users.Any(u => u.Id == userId))
                    break;

                Console.WriteLine("ID inválido. Intente de nuevo.");
            }

            Console.Write("Título del proyecto: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Descripción del proyecto: ");
            string description = Console.ReadLine() ?? "";

            // Validar área
            var areas = await _areaQuery.GetAllAreas();
            Console.WriteLine("Áreas disponibles:");
            foreach (var area in areas)
            {
                Console.WriteLine($"- {area.Name}");
            }

            string areaName;
            int areaId;
            while (true)
            {
                Console.Write("Ingrese el nombre del área: ");
                areaName = Console.ReadLine() ?? "";
                var area = areas.FirstOrDefault(a => a.Name.Equals(areaName, StringComparison.OrdinalIgnoreCase));
                if (area != null)
                {
                    areaId = area.Id;
                    break;
                }
                Console.WriteLine("Área inválida. Intente nuevamente.");
            }

            // Validar tipo de proyecto
            var types = await _typeQuery.GetAllTypes();
            Console.WriteLine("Tipos de proyecto disponibles:");
            foreach (var type in types)
            {
                Console.WriteLine($"- {type.Name}");
            }

            string typeName;
            int typeId;
            while (true)
            {
                Console.Write("Ingrese el nombre del tipo de proyecto: ");
                typeName = Console.ReadLine() ?? "";
                var type = types.FirstOrDefault(t => t.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
                if (type != null)
                {
                    typeId = type.Id;
                    break;
                }
                Console.WriteLine("Tipo inválido. Intente nuevamente.");
            }

            // Monto estimado
            decimal estimatedAmount;
            while (true)
            {
                Console.Write("Monto estimado del proyecto: ");
                if (decimal.TryParse(Console.ReadLine(), out estimatedAmount) && estimatedAmount > 0)
                    break;

                Console.WriteLine("Monto inválido. Intente nuevamente.");
            }

            // Duración estimada
            int estimatedDuration;
            while (true)
            {
                Console.Write("Duración estimada en días: ");
                if (int.TryParse(Console.ReadLine(), out estimatedDuration) && estimatedDuration > 0)
                    break;

                Console.WriteLine("Duración inválida. Intente nuevamente.");
            }

            // Crear la entidad
            var proposal = new ProjectProposal
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                EstimatedAmount = estimatedAmount,
                EstimatedDuration = estimatedDuration,
                CreateAt = DateTime.Now,
                CreateBy = userId,
                Area = areaId,
                Type = typeId,
                Status = 1
            };

            await _command.createAsync(proposal);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSolicitud de proyecto creada con éxito.");
            Console.ResetColor();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
