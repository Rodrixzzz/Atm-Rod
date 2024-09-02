using Atm_Rod_Entities.Entity;
using Atm_Rod_Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Interface.Services
{
    public interface IJwtService
    {
        ResponseLogin GenerateToken(int Number);

        bool ValidateToken(string token);
    }
}
