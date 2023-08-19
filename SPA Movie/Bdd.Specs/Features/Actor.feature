Feature: Actor

@addactor
Scenario: Adding an actor
    Given I am a client
    When I am making a post request to '<resourceEndpoint>' with the following Data '<Data>'
    Then response code must be '<statusCode>'
    And response data must look like '<responseData>'
    Examples: 
    | Data                                                                                     | resourceEndpoint | statusCode | responseData      |
    | { "name":"Anushka","bio": "AnushkaBio","dob": "2001-12-02T00:00:00","gender": "female" } | actors/AddActor  | 201        | 1                 |
    | {"name":"","bio":"AnushkaBio","dob":"2001-12-02T00:00:00","gender":"female"}             | actors/AddActor  | 400        | invalid Arguments |
    | {"name":"Anushka","bio":"","dob":"2001-12-02T00:00:00","gender":"female"}                | actors/AddActor  | 400        | invalid Arguments |
    | {"name":"Anushka","bio":"AnushkaBio","dob":"2001-12-02T00:00:00","gender":""}            | actors/AddActor  | 400        | invalid Arguments |
                                                                                          
                                                                                         
@displayactors
Scenario:Display the list of Actors
         Given I am a client
         When I make GET Request '<resourceEndpoint>' 
         Then  response code must be '<statusCode>'
         And response data must look like '<responseData>'
         Examples:
         | resourceEndpoint    | statusCode | responseData                                                                                                                                                                                    |
         | actors/GetAllActors | 200        | [{"id":1,"name":"Anushka","bio":"AnushkaBio","dob":"2001-12-02T00:00:00","gender":"female"},{"id":2,"name":"Prabhas","bio":"PrabhasBio","dob":"2000-08-04T00:00:00","gender":"male"}] |
        

       
@displayactor
Scenario:Display the actor by using id
         Given I am a client
         When I make GET Request using Id '<resourceEndpoint>'
         Then  response code must be '<statusCode>'
         And response data must look like '<responseData>'
         Examples:
          | resourceEndpoint        | statusCode | responseData                                                                               |
          | actors/GetActorById/1   | 200        | {"id":1,"name":"Anushka","bio":"AnushkaBio","dob":"2001-12-02T00:00:00","gender":"female"} |
          | actors/GetActorById/100 | 404        | No Content Found                                                                           |
          | actors/GetActorById/-1  | 400        | invalid Arguments                                                                          |
          | actors/GetActorById/0   | 400        | invalid Arguments                                                                          |


@deleteactor
Scenario:Delete the actor using id
         Given I am a client
         When I make Delete Request '<resourceEndpoint>'
         Then  response code must be '<statusCode>'
         And response data must look like '<responseData>'
         Examples: 
            | resourceEndpoint       | statusCode | responseData         |
            | actors/DeleteActor/1   | 200        | deleted successfully |
            | actors/DeleteActor/-1  | 400        | invalid Arguments    |
            | actors/DeleteActor/0   | 400        | invalid Arguments    |
            | actors/DeleteActor/100 | 404        | No Content Found     |
    

          
         
@updateactor
Scenario:update the actor using id
          Given I am a client
          When I make PUT Request '<resourceEndpoint>' with the following Data with the following Data '<Data>'
          Then  response code must be '<statusCode>'
          And response data must look like '<responseData>'
          Examples:     
            | Data                                                                                         | resourceEndpoint      | statusCode | responseData         |
            | {"name":"Anushka","bio": "AnushkaBio","dob": "1989-05-12T15:07:39.346Z","gender": "female" } | actors/UpdateActor/1  | 200        | updated successfully |
            | {"name":"","bio":"AnushkaBio","dob":"1989-05-12T15:07:39.346Z","gender":"female"}            | actors/UpdateActor/1  | 400        | invalid Arguments    |
            | {"name":"Anushka","bio":"","dob":"1989-05-12T15:07:39.346Z","gender":"female"}               | actors/UpdateActor/1  | 400        | invalid Arguments    |
            | {"name":"Anushka","bio":"AnushkaBio","dob":"1989-05-12T15:07:39.346Z","gender":""}           | actors/UpdateActor/1  | 400        | invalid Arguments    |
            | {"name":"Anushka","bio":"AnushkaBio","dob":"1989-05-12T15:07:39.346Z","gender":"female"}     | actors/UpdateActor/-1 | 400        | invalid Arguments    |
            | {"name":"Anushka","bio":"AnushkaBio","dob":"1989-05-12T15:07:39.346Z","gender":"female"}     | actors/UpdateActor/8  | 404        | No Content Found     |

 
         
   

