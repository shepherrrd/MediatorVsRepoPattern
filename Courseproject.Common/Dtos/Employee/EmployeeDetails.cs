using Courseproject.Common.Dtos.Address;
using Courseproject.Common.Dtos.Job;
using Courseproject.Common.Dtos.Teams;

namespace Courseproject.Common.Dtos.Employee;

//todo: add teams
public record EmployeeDetails(int Id, string FirstName, string LastName, AddressGet Address, JobGet Job/*, List<TeamGet> Teams*/);