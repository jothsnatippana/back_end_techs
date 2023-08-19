using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;
using EMS.Repositorylayer;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static System.Net.Mime.MediaTypeNames;

namespace EMS.servicelayer
{
    public class recommendationresponseTeamLead
    {
        public recommendationresponseTeamLead(dynamic Id1, dynamic Name1, dynamic Role1, dynamic TeamId1)
        {
            Id = Id1;
            Name = Name1;
            Role = Role1;
            TeamId = TeamId1;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public int TeamId { get; set; }
    }

    public class recommendationresponseSenior
    {
        public recommendationresponseSenior(dynamic Id1, dynamic Name1, dynamic Role1)
        {
            Id = Id1;
            Name = Name1;
            Role = Role1;
           
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
    public class Recommendation
    {

        teamrepo createteam = new teamrepo();
        public async Task<int> getrecommendationsAsync()
        {
            // case a
            const string query = @"SELECT TOP 1 CurrentTeamId
	                               ,count(*)
                            FROM Foundation.Employees AS emp
                            LEFT JOIN Foundation.Roles AS ROLE ON ROLE.Id = emp.Roleid
                            WHERE ROLE.Name = 'junior'
                            GROUP BY CurrentTeamId
                            HAVING count(*) < 2";
            // case b
            const string query1 = @"SELECT emp.Id AS Id
	                                ,emp.Name AS Name
	                                ,ROLE.Name AS Role
	                                ,empteam.TeamId AS past_team_assignments
                                FROM Foundation.Employees AS emp
                                LEFT JOIN Foundation.Roles AS ROLE ON ROLE.Id = emp.Roleid
                                LEFT JOIN Foundation.Employees_Teams AS empteam ON empteam.EmployeeId = emp.Id
                                WHERE ROLE.Name = 'Senior'
                                ORDER BY emp.DateOfJoining ASC";
            //
            const string query2 = @"SELECT emp.Id AS Id
	                                ,emp.Name AS Name
	                                ,ROLE.Name AS Role
                                FROM Foundation.Employees AS emp
                                LEFT JOIN Foundation.Roles AS ROLE ON ROLE.Id = emp.Roleid
                                WHERE ROLE.Name = 'junior' 
                                ORDER BY emp.DateOfJoining ASC;";
            try
            {
                using (var connection = new SqlConnection("Data Source = DESKTOP-5BJE6IP; Initial Catalog = EmployeeManagementSystem; Integrated Security = True; Encrypt = False;"))
                {
                    connection.Open();
                    dynamic result = await connection.QueryAsync(query);
                    if (result.Count==0 || result == null)
                    {
                        dynamic result1 = await connection.QueryAsync(query1);
                        List<recommendationresponseTeamLead> employeeslist = new List<recommendationresponseTeamLead>();
                        foreach (var item in result1)
                        {
                            employeeslist.Add(new recommendationresponseTeamLead(item.Id, item.Name, item.Role, item.past_team_assignments));
                        }
                        var maxCountGroup = employeeslist
                                                        .GroupBy(e => e.Id)
                                                        .Select(g => new { Id = g.Key, Count = g.Count() })
                                                        .OrderByDescending(g => g.Count)
                                                        .FirstOrDefault();
                        if (maxCountGroup == null || maxCountGroup.Count<2)
                            return 0;
                        else
                        {
                            dynamic result2 = await connection.QueryAsync(query2);
                            if (result2.Count == 0 || result2 == null)
                                return 0;
                            List<recommendationresponseSenior> Seniorslist = new List<recommendationresponseSenior>();
                            foreach (var item in result2)
                            {
                                Seniorslist.Add(new recommendationresponseSenior(item.Id, item.Name, item.Role));
                            }
                            List<int> members= new List<int>();
                            members.Add(maxCountGroup.Id);
                            members.Add(Seniorslist[0].Id);
                            return await createteam.CreateTeamAsync(new requestteam ("newteam",members));
                        }
                        
                    }
                    int m = result[0].CurrentTeamId;
                    return m;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

                    
    }
}

