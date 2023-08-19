using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;
using System.Linq;
using Dapper;

namespace EMS.Repositorylayer
{
    public class teamrepo
    {
        public async Task<int> CreateTeamAsync(requestteam req)
        {
           
            const string query = @"
                                 insert into Foundation.Teams(Name) values(@name);
                                 SELECT CAST(scope_identity() AS int);
                                  ";
            const string query2 = @"update Foundation.Employees set currentTeamid=@id where Id in @list";
            try
            {
                using (var connection = new SqlConnection("Data Source = DESKTOP-5BJE6IP; Initial Catalog = EmployeeManagementSystem; Integrated Security = True; Encrypt = False;"))
                {
                    connection.Open();
                    dynamic result = await connection.ExecuteScalarAsync(query, new { name=req.name });
                    int res = result;
                    await connection.ExecuteAsync(query2, new { id = res , list=req.members});
                    return res;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
