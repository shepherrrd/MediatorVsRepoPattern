using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courseproject.Common.Model;

public class Job : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
