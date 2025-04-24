using Microsoft.EntityFrameworkCore;
using Infraestructure.Persistence;
using Infraestructure.Query;
using Infraestructure.Command;
using Aplication.Interfaces;
using Aplication.Services;
using PracticaAprobacionDeProyectos;

var context = new AppDbContext();

//instancia de las queries
IUserQuery userQuery = new UserQuery(context);
IAreaQuery areaQuery = new AreaQuery(context);
IProjectTypeQuery projectTypeQuery = new ProjectTypeQuery(context);
IApprovalRuleQuery ruleQuery = new ApprovalRuleQuery(context);
IProjectProposalQuery proposalQuery = new ProjectProposalQuery(context);
IApprovalRuleQuery approvalRuleQuery = new ApprovalRuleQuery(context);
IProjectApprovalStepQuery approvalStepQuery = new ProjectApprovalStepQuery(context);

//instancia de los commands
IProjectProposalCommand projectProposalCommand = new ProjectProposalCommand(context);
IProjectApprovalStepCommand projectApprovalStepCommand = new ProjectApprovalStepCommand(context);

//instancia de los services
IProjectProposalService projectProposalService = new ProjectProposalService(projectProposalCommand);
IProjectApprovalStepService projectApprovalStepService = new ProjectApprovalStepService(ruleQuery, userQuery, approvalStepQuery, proposalQuery, projectApprovalStepCommand);
IUserRoleService userRoleService = new UserRoleService(userQuery);

//instancia del menu y ejecucion
Menu menu = new Menu(projectProposalService, projectApprovalStepService, userQuery, areaQuery, projectTypeQuery, proposalQuery, userRoleService, approvalStepQuery);
await menu.ShowLoginAsync();