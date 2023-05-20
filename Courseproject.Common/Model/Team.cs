using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courseproject.Common.Model;

public class Team : BaseEntity
{
    public string Name { get; set; } = default!;
    public List<Employee> Employees { get; set; } = default!;
}
