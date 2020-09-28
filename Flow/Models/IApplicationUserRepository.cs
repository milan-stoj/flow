using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flow.Models
{
    public interface IApplicationUserRepository
    {   
        ApplicationUser GetApplicationUser(string id);
    }
}
