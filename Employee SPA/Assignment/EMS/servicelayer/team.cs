using EMS.Repositorylayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.servicelayer
{
    public class team
    {
        public team() { }
        teamrepo emrepo = new teamrepo();
        public async Task<int> createteam(requestteam req)
        { 
            return await emrepo.CreateTeamAsync(req);
        }
    }
}
