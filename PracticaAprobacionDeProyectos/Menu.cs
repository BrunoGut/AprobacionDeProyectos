using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Aplication.Services;
using Aplication.Exceptions;

namespace PracticaAprobacionDeProyectos
{
    public class Menu
    {
        private readonly IProjectProposalService _proposalService;
        private readonly IProjectApprovalStepService _stepService;
        private readonly IUserQuery _userQuery;
        private readonly IAreaQuery _areaQuery;
        private readonly IProjectTypeQuery _typeQuery;
        private readonly IProjectProposalQuery _proposalQuery;

        public Menu(IProjectProposalService proposalService, IProjectApprovalStepService stepService, IUserQuery userQuery, IAreaQuery areaQuery, IProjectTypeQuery typeQuery, IProjectProposalQuery proposalQuery)
        {
            _proposalService = proposalService;
            _stepService = stepService;
            _userQuery = userQuery;
            _areaQuery = areaQuery;
            _typeQuery = typeQuery;
            _proposalQuery = proposalQuery;
        }

        public async Task ShowLoginAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("----- SISTEMA DE APROBACIÓN DE PROYECTOS -----");
                Console.WriteLine("Seleccione un usuario para iniciar sesión:");

                var users = await _userQuery.GetAllAsync();
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Id} - {user.Name}");
                }
                Console.WriteLine("0 - Salir");

                Console.Write("Ingrese el ID del usuario: ");
                if (int.TryParse(Console.ReadLine(), out int selectedUserId))
                {
                    if (selectedUserId == 0)
                    {
                        Console.WriteLine("Saliendo del sistema...");
                        return;
                    }

                    var selectedUser = users.FirstOrDefault(u => u.Id == selectedUserId);
                    if (selectedUser != null)
                    {
                        await ShowUserSubMenuAsync(selectedUserId, selectedUser.Name);
                    }
                    else
                    {
                        Console.WriteLine("ID de usuario inválido. Presione una tecla para continuar.");
                        Console.ReadKey();
                    }
                }
            }
        }

        private async Task ShowUserSubMenuAsync(int userId, string userName)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"----- BIENVENIDO, {userName.ToUpper()} -----");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1 - Crear propuesta de proyecto");
                Console.WriteLine("2 - Aprobar / Rechazar / Observar propuesta");
                Console.WriteLine("3 - Ver estado de una propuesta");
                Console.WriteLine("4 - Eliminar una propuesta");
                Console.WriteLine("5 - Cambiar de usuario");
                Console.WriteLine("0 - Salir");

                Console.Write("Opción: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await CreateProposalAsync(userId);
                        break;
                    case "2":
                        //implementar
                        break;
                    case "3":
                        //implementar
                        break;
                    case "4":
                        //implementar
                        break;
                    case "5":
                        return;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione una tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task CreateProposalAsync(int userId)
        {
            Console.Clear();
            Console.WriteLine("----- CREAR PROPUESTA DE PROYECTO -----");

            Console.Write("Título del proyecto: ");
            string title = Console.ReadLine();

            Console.Write("Descripción del proyecto: ");
            string description = Console.ReadLine();

            decimal amount;
            do
            {
                Console.Write("Monto estimado: ");
            } while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0);

            int duration;
            do
            {
                Console.Write("Duración estimada (en días): ");
            } while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 1);

            var areas = await _areaQuery.GetAllAsync();
            Console.WriteLine("\nÁreas disponibles:");
            foreach (var area in areas)
                Console.WriteLine($"{area.Id} - {area.Name}");

            int areaId;
            do
            {
                Console.Write("ID del área: ");
            } while (!int.TryParse(Console.ReadLine(), out areaId) || !areas.Any(a => a.Id == areaId));

            var types = await _typeQuery.GetAllAsync();
            Console.WriteLine("\nTipos de proyecto disponibles:");
            foreach (var type in types)
                Console.WriteLine($"{type.Id} - {type.Name}");

            int typeId;
            do
            {
                Console.Write("ID del tipo de proyecto: ");
            } while (!int.TryParse(Console.ReadLine(), out typeId) || !types.Any(t => t.Id == typeId));

            // Crear la propuesta
            var proposal = await _proposalService.CreateWithAssignmentAsync(
                title, description, areaId, typeId, amount, duration, userId
            );

            // Generar los pasos y obtener los asignados
            var steps = await _stepService.GenerateStepsAsync(proposal);

            // Mostrar resumen de asignaciones
            Console.WriteLine("\nPropuesta creada exitosamente.");
            Console.WriteLine("\nSe asignaron los siguientes pasos de aprobación:");
            foreach (var step in steps)
            {
                if (step.ApproverUserId.HasValue)
                {
                    var assignedUser = await _userQuery.GetByIdAsync(step.ApproverUserId.Value);
                    Console.WriteLine($"Paso {step.StepOrder}: asignado a {assignedUser.Name} (ID: {assignedUser.Id})");
                }
                else
                {
                    Console.WriteLine($"Paso {step.StepOrder}: sin usuario asignado.");
                }

            }

            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }

    }

}
