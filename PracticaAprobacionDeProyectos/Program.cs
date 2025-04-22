using Microsoft.EntityFrameworkCore;
using Infraestructure.Persistence;
using Infraestructure.Query;
using Infraestructure.Command;
using Aplication.Interfaces;
using Aplication.Services;
using PracticaAprobacionDeProyectos;

//var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
//optionsBuilder.UseSqlServer("Server=DESKTOP-O1PN00U\\SQLEXPRESS;Database=AprobacionProyectosDB;Trusted_Connection=True;TrustServerCertificate=True;");

var context = new AppDbContext();

//instancia de las queries
IUserQuery userQuery = new UserQuery(context);
IAreaQuery areaQuery = new AreaQuery(context);
IProjectTypeQuery projectTypeQuery = new ProjectTypeQuery(context);
IApprovalRuleQuery ruleQuery = new ApprovalRuleQuery(context);
IProjectProposalQuery proposalQuery = new ProjectProposalQuery(context);
IApprovalRuleQuery approvalRuleQuery = new ApprovalRuleQuery(context);

//instancia de los commands
IProjectProposalCommand projectProposalCommand = new ProjectProposalCommand(context);
IProjectApprovalStepCommand projectApprovalStepCommand = new ProjectApprovalStepCommand(context);

//instancia de los services
IProjectProposalService projectProposalService = new ProjectProposalService(projectProposalCommand);
IProjectApprovalStepService projectApprovalStepService = new ProjectApprovalStepService(ruleQuery, userQuery, projectApprovalStepCommand);

//instancia del menu y ejecucion
Menu menu = new Menu(projectProposalService, projectApprovalStepService, userQuery, areaQuery, projectTypeQuery, proposalQuery);
await menu.ShowLoginAsync();