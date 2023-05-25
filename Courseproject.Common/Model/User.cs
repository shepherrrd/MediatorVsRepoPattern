using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courseproject.Common.Model
{
    public class User 
    {
        public string username { get; set; } = default!;
        public string email { get; set; } = default!;
        public string password {get; set;} = default!;
        
    }
}