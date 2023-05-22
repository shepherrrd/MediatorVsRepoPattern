using AutoMapper;
using Courseproject.Common.Dtos.Teams;
using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;
using System.Linq.Expressions;

namespace Courseproject.Business.Services;

public class TeamService : ITeamService
{
    private IGenericRepository<Team> TeamRepository { get; }
    private IGenericRepository<Employee> EmployeeRepository { get; }
    private IMapper Mapper { get; }

    public TeamService(IGenericRepository<Team> teamRepository, IGenericRepository<Employee> employeeRepository,
        IMapper mapper)
    {
        TeamRepository = teamRepository;
        EmployeeRepository = employeeRepository;
        Mapper = mapper;
    }


    public async Task<int> CreateTeamAsync(TeamCreate teamCreate)
    {
        Expression<Func<Employee, bool>> employeeFilter = (employee) => teamCreate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);
        var entity = Mapper.Map<Team>(teamCreate);
        entity.Employees = employees;
        await TeamRepository.InsertAsync(entity);
        await TeamRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteTeamAsync(TeamDelete teamDelete)
    {
        var entity = await TeamRepository.GetByIdAsync(teamDelete.Id);
        TeamRepository.Delete(entity);
        await TeamRepository.SaveChangesAsync();
    }

    public async Task<TeamGet> GetTeamAsync(int id)
    {
        var entity = await TeamRepository.GetByIdAsync(id, (team) => team.Employees);
        return Mapper.Map<TeamGet>(entity);
    }

    public async Task<List<TeamGet>> GetTeamsAsync()
    {
        var entities = await TeamRepository.GetAsync(null, null, (team) => team.Employees);
        return Mapper.Map<List<TeamGet>>(entities);
    }

    public async Task UpdateTeamAsync(TeamUpdate teamUpdate)
    {
        Expression<Func<Employee, bool>> employeeFilter = (employee) => teamUpdate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);
        var existingEntity = await TeamRepository.GetByIdAsync(teamUpdate.Id, (team) => team.Employees);
        Mapper.Map(teamUpdate, existingEntity);
        existingEntity.Employees = employees;
        TeamRepository.Update(existingEntity);
        await TeamRepository.SaveChangesAsync();
    }
}
