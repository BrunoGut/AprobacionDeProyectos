using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Aplication.Dtos;

namespace PracticaAprobacionDeProyectos
{
    public class Menu
    {
        private readonly IProjectProposalService _projectProposalService;
        private readonly IProjectApprovalStepService _projectApprovalStepService;

        public Menu(IProjectProposalService projectProposalService, IProjectApprovalStepService projectApprovalStepService)
        {
            _projectProposalService = projectProposalService;
            _projectApprovalStepService = projectApprovalStepService;
        }

        public async Task showMenu()
        {
            bool continuar = true;
            while(continuar)
            {
                Console.Clear();
                showOptions();

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        await createRequest();
                        break;

                    case "2":
                        await processApproval();
                        break;

                    case "3":
                        await viewStatus();
                        break;

                    case "0":
                        continuar = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opcion invalida. Presione una tecla para continuar...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }

            }
        }

        private void showOptions()
        {
            Console.WriteLine("============== MENÚ DE PROYECTOS ==============");
            Console.WriteLine("1) Crear una solicitud de proyecto.");
            Console.WriteLine("2) Aprobar/Rechazar solicitud de proyecto.");
            Console.WriteLine("3) Ver estado de una solicitud.");
            Console.WriteLine("0) Salir.");
        }

        private async Task createRequest()
        {
            Console.Clear();
            Console.WriteLine("=== Crear solicitud de proyecto ===\n");

            try
            {
                await _projectProposalService.CreateRequestAsync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ Error: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }
        }

        private async Task processApproval()
        {

        }

        private async Task viewStatus()
        {

        }
    }
}
