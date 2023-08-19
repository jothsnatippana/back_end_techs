using System.Threading.Tasks;
using EMS.Repositorylayer;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace EMS.servicelayer
{
    public class employee
    {
        public employee() { }
        employeerepo emrepo = new employeerepo();
        public async Task<IEnumerable<responseEmployee>> getbyidAsync(int id)
        { 
           return await emrepo.GetByIdAsync(id);
        }
    }
}
