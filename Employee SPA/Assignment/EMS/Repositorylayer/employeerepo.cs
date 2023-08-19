using System.Threading.Tasks;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using EMS.servicelayer;
using Dapper;



using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting.Server;
using static System.Net.Mime.MediaTypeNames;

namespace EMS.Repositorylayer
{
    public class employeerepo
    {
        public async Task<List<responseEmployee>> GetByIdAsync(int id)
        {
            const string query = @"
                                 SELECT  emp.Name as name
	                                    ,role.Name as role
                                 FROM Foundation.Employees as emp
                                 left join Foundation.Roles as role 
                                 on emp.roleId=role.Id
                                 where emp.currentteamid=@id";
            try
            {
                using (var connection = new SqlConnection("Data Source = DESKTOP-5BJE6IP; Initial Catalog = EmployeeManagementSystem; Integrated Security = True; Encrypt = False;"))
                {
                    connection.Open();
                     dynamic result= await connection.QueryAsync(query, new { Id = id });
                    List<responseEmployee> list = new List<responseEmployee>();
                    foreach(var item in result)
                    {
                        list.Add(new responseEmployee(item.name, item.role));
                    }
                    return list;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
