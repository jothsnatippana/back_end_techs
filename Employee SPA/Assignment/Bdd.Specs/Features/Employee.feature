Feature: employee

@displayemployees
Scenario: Get All Employees using required roleid
	Given I am a user
    When I make a get request to '<resourceEndpoint>'
    Then response code must be '<statusCode>'
    And response data must look like '<responseData>'
    | resourceEndpoint                               | statusCode | responseData                                                                                                     |
    | employees/getemployeebyroleids?roleid=1,2      | 201        | [{"name": "anu" ,"role" : "senior"},{"name": "samuel" ,"role" : "junior"},{"name": "ranjit" ,"role" : "senior"}] |
    | employees/getemployeebyroleids?roleid=1        | 201        | [{"name": "anu" ,"role" : "senior"},{"name": "ranjit" ,"role" : "senior"}]                                       |
    | employees/getemployeebyroleids/?roleid=-2,2000 | 404        | No Content FOund                                                                                                 |
 
@createteams
Scenario: creating a new team along with team members
 Given I am a user
 When I am making a post request to '<resourceEndpoint>' with the following Data '<Data>'
 Then response code must be '<statusCode>'
 And response data must look like '<responseData>'
   | Data                                            | resourceEndpoint | statusCode | responseData      |
   | {"name":"DevelopmentTeam","memberids":[1,7,10]} | teams/createteam | 201        | 1                 |
   | {"name":"","memberids":[2,5]}                   | actors/AddActor  | 400        | invalid Arguments |
   | {"name":"Anushka","memberids":[]}               | actors/AddActor  | 400        | invalid Arguments |
   | {"name":"Anushka","memberids":[2000,2]}         | actors/AddActor  | 404        | No Content Found  |

@addmembers

Scenario: adding employees to specific teams
 Given Iam a user
 When I am making a post request to '<resourceEndpoint>' with the following Data '<Data>'
 Then response code must be '<statusCode>'
 And response data must look like '<responseData>'
  | Data                   | resourceEndpoint    | statusCode | responseData       |
  | {"memberids":[1,7,10]} | teams/addmembers/1  | 201        | added successfully |
  | {"memberids":[1,7,10]} | teams/addmembers/-1 | 404        | No COntent Found   |
  | {"memberids":[]}       | teams/addmembers/1  | 400        | invalid Arguments  |
                                                                                         
@updateteamleads

Scenario: updating teamleads for a specific team
Given I am a user
When Iam making a patch request to '<resourceEndpoint>' with the following Data '<Data>'
Then response code must be '<statusCode>'
And response data must look like '<responseData>'
 | Data                                     | resourceEndpoint        | statusCode | responseData         |
 | {"employeeid":2,"currentteamid": 4}      | employees/updateleads/4 | 201        | updated successfully |
 | {"employeeid":-1 ,"currentteamid": 4}    | employees/updateleads/4 | 400        | invalid Arguments    |
 | {"employeeid":2,"currentteamid": -4}     | employees/updateleads/4 | 400        | invalid Arguments    |
 | {"employeeid":2000,"currentteamid": 400} | employees/updateleads/4 | 400        | No Content Found     |

 @getemployees

 Scenario: getting employees from a specific team

 Given iam a user
 When i make a get request to '<resourceEndpoint>' 
 Then response code must be '<statusCode>'
 And response data must look like '<responseData>'
 | resourceEndpoint                   | statusCode | responseData                                                                                                     |
 | employees/getemployeebyteamids/1   | 201        | [{"name": "anu" ,"role" : "senior"},{"name": "samuel" ,"role" : "junior"},{"name": "ranjit" ,"role" : "senior"}] |
 | employees/getemployeebyrteamids/-1 | 201        | invalid Arguments                                                                                                |
 | employees/getemployeebyteamids/200 | 404        | No Content FOund                                                                                                 |

