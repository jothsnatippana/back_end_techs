Feature: genre

@addgenre
Scenario: Adding an genre
    Given I am a client
    When I am making a post request to '<resourceEndpoint>' with the following Data '<Data>'
    Then response code must be '<statusCode>'
    And response data must look like '<responseData>'
    Examples: 
    | Data             | resourceEndpoint | statusCode | responseData      |
    | {"name":"Crime"} | genres/AddGenre  | 201        | 1                 |
    | {"name":""}      | genres/AddGenre  | 400        | invalid Arguments |
                                                                                          
                                                                                         
@displaygenres
Scenario:Display the list of genres
         Given I am a client
         When I make GET Request '<resourceEndpoint>' 
         Then  response code must be '<statusCode>'
         And response data must look like '<responseData>'
         Examples:
         | resourceEndpoint   | statusCode | responseData                                       |
         | genres/GetAllGenre | 200        | [{"id":1,"name":"Crime"},{"id":2,"name":"Action"}] |
         

       
@displaygenre
Scenario:Display the genre by using id
         Given I am a client
         When I make GET Request using Id '<resourceEndpoint>'
         Then  response code must be '<statusCode>'
         And response data must look like '<responseData>'
         Examples:
          | resourceEndpoint        | statusCode | responseData            |
          | genres/GetGenreById/1   | 200        | {"id":1,"name":"Crime"} |
          | genres/GetGenreById/100 | 404        | No Content Found        |
          | genres/GetGenreById/-1  | 400        | invalid Arguments       |
          | genres/GetGenreById/0   | 400        | invalid Arguments       |

@deletegenre
Scenario:Delete the genre using id
         Given I am a client
         When I make Delete Request '<resourceEndpoint>'
         Then  response code must be '<statusCode>'
         And response data must look like '<responseData>'
         Examples: 
            | resourceEndpoint      | statusCode | responseData         |
            | genres/DeleteGenre/1  | 200        | deleted successfully |
            | genres/DeleteGenre/-1 | 400        | invalid Arguments    |
            | genres/DeleteGenre/0  | 400        | invalid Arguments    |
            | genres/DeleteGenre/90 | 404        | No Content Found     |

         
@updategenre
Scenario:update the genre using id
          Given I am a client
          When I make PUT Request '<resourceEndpoint>' with the following Data with the following Data '<Data>'
          Then  response code must be '<statusCode>'
          And response data must look like '<responseData>'
          Examples:     
            | Data                 | resourceEndpoint       | statusCode | responseData         |
            | { "name":"Rom-Com" } | genres/UpdateGenre/1   | 200        | updated successfully |
            | {"name":""}          | genres/UpdateGenre/1   | 400        | invalid Arguments    |
            | {"name":"Crime"}     | genres/UpdateGenre/-1  | 400        | invalid Arguments    |
            | {"name":"Crime"}     | genres/UpdateGenre/100 | 404        | No Content Found     |
   


 
         
   

