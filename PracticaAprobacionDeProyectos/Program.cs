using Infraestructure.Persistence;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infraestructure.Query;
using Aplication.Services;
using Infraestructure.Command;
using PracticaAprobacionDeProyectos;
using Aplication.Interfaces;

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer("Server=DESKTOP-O1PN00U\\SQLEXPRESS;Database=AprobacionProyectosDB;Trusted_Connection=True;TrustServerCertificate=True;")
    .Options;

//inicializacion de dependencias
AppDbContext dbContext = new AppDbContext(options);

//instancia de las queries
IUserQuery userQuery = new UserQuery(dbContext);
IAreaQuery areaQuery = new AreaQuery(dbContext);
IProjectTypeQuery projectTypeQuery = new ProjectTypeQuery(dbContext);

//instancia de los commands
IProjectProposalCommand projectProposalCommand = new ProjectProposalCommand(dbContext);
IProjectApprovalStepCommand projectApprovalStepCommand = new ProjectApprovalStepCommand(dbContext);

//instancia de los services
IProjectProposalService projectProposalService = new ProjectProposalService(projectProposalCommand, userQuery, areaQuery, projectTypeQuery);
IProjectApprovalStepService projectApprovalStepService = new ProjectApprovalStepService(projectApprovalStepCommand);

//instancia del menu y ejecucion
Menu menu = new Menu(projectProposalService, projectApprovalStepService);
await menu.showMenu();