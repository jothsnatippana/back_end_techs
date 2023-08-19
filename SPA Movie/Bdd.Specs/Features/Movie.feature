Feature: movie

@addmovie
Scenario: Adding an movie
    Given I am a client
    When I am making a post request to '<resourceEndpoint>' with the following Data '<Data>'
    Then response code must be '<statusCode>'
    And response data must look like '<responseData>'
    Examples: 
    | Data                                                                                                                                                  | resourceEndpoint | statusCode | responseData                                         |
    | {"name": "Bahubali","yearOfRelease": 2021,"plot": "Bahubali_plot","actorIds": [1,2],"genreIds": [3,4],"producedBy": 1,"coverImage": "bahubaliPoster"} | movies/AddMovie  | 201        | 1                                                    |
    | {"name": "","yearOfRelease": 2021,"plot": "Bahubali_plot","actorIds": [1,2],"genreIds": [3,6],"producedBy": 1,"coverImage": "bahubaliPoster"}         | movies/AddMovie  | 400        | invalid Arguments                                    |
    | {"name": "Bahubali","yearOfRelease":201 ,"plot": "Bahubali_plot","actorIds": [1,2],"genreIds": [3,6],"producedBy": 1,"coverImage": "bahubaliPoster"}  | movies/AddMovie  | 400        | invalid Arguments                                    |
    | {"name": "Bahubali","yearOfRelease": 2021,"plot": "Bahubali_plot","actorIds": [4,5],"genreIds": [1,2],"producedBy": 1,"coverImage": "bahubaliPoster"} | movies/AddMovie  | 400        | Actorids/generids cannot be found , enter valid data |
    | {"name": "Bahubali","yearOfRelease": 2021,"plot": "Bahubali_plot","actorIds": [1,2],"genreIds": [4,5],"producedBy": 1,"coverImage": "bahubaliPoster"} | movies/AddMovie  | 400        | Actorids/generids cannot be found , enter valid data |
    | {"name": "Bahubali","yearOfRelease": 2021,"plot": "Bahubali_plot","actorIds": [1,2],"genreIds": [1,2],"producedBy": 1,"coverImage":""}                | movies/AddMovie  | 400        | invalid Arguments                                    |
    | {"name": "Bahubali","yearOfRelease": 2021,"plot": "Bahubali_plot","actorIds": [1,2],"genreIds": [3,4],"producedBy": 5,"coverImage": "bahubaliPoster"} | movies/AddMovie  | 400        | Producer id couldn't be found                        |
@displaymovies
Scenario:Display the list of movies
         Given I am a client
         When I make GET Request '<resourceEndpoint>' 
         Then  response code must be '<statusCode>'
         And response data must look like '<responseData>'
         Examples:
         | resourceEndpoint    | statusCode | responseData                                                                                                                                                                                                                                                                                                                                                                     |
         | movies/GetAllmovies | 200        | [{"id":1,"name":"KGFChap2","yearOfRelease":2022,"plot":"KGFPlot","actors":["Yash","Shrinidhi"],"genres":["Comedy","Thriller"],"producers":"Priyanka","coverImage":"KGFChap2CoverPage"},{"id":2,"name":"Kantara","yearOfRelease":2022,"plot":"KantaraPlot","actors":["Ramacharan","Vinay"],"genres":["Adventure","Action"],"producers":"Shobu","coverImage":"KantaraCoverPage"}] |
       

       
@displaymovie
Scenario:Display the movie by using id
         Given I am a client
         When I make GET Request using Id '<resourceEndpoint>'
         Then  response code must be '<statusCode>'
         And response data must look like '<responseData>'
         Examples:
          | resourceEndpoint         | statusCode | responseData                                                                                                                                                                           |
          | movies/GetMoviesById/1   | 200        | {"id":1,"name":"KGFChap2","yearOfRelease":2022,"plot":"KGFPlot","actors":["Yash","Shrinidhi"],"genres":["Comedy","Thriller"],"producers":"Priyanka","coverImage":"KGFChap2CoverPage"} |
          | movies/GetMoviesById/100 | 404        | No Content Found                                                                                                                                                                       |
          | movies/GetMoviesById/-1  | 400        | invalid Arguments                                                                                                                                                                      |
          | movies/GetMoviesById/0   | 400        | invalid Arguments                                                                                                                                                                      |
                                                                 


@deletemovie
Scenario:Delete the movie using id
         Given I am a client
         When I make Delete Request '<resourceEndpoint>'
         Then  response code must be '<statusCode>'
         And response data must look like '<responseData>'
         Examples: 
            | resourceEndpoint       | statusCode | responseData         |
            | movies/DeleteMovie/1   | 200        | deleted successfully |
            | movies/DeleteMovie/-1  | 400        | invalid Arguments    |
            | movies/DeleteMovie/0   | 400        | invalid Arguments    |
            | movies/DeleteMovie/100 | 404        | No Content Found     |

         
@updatemovie
Scenario:update the movie using id
          Given I am a client
          When I make PUT Request '<resourceEndpoint>' with the following Data with the following Data '<Data>'
          Then  response code must be '<statusCode>'
          And response data must look like '<responseData>'
          Examples:     
            | Data                                                                                                                                               | resourceEndpoint       | statusCode | responseData                                         |
            | {"name":"Bahubali","yearOfRelease":2021,"plot":"Bahubali_plot","actorIds":[1,2],"genreIds":[3,4],"producedBy":1,"coverImage":"bahubaliPoster"}     | movies/UpdateMovie/1   | 200        | updated successfully                                 |
            | {"name":"Bahubali","yearOfRelease":20,"plot":"Bahubali_plot","actorIds": [1,2],"genreIds": [3,4],"producedBy": 1,"coverImage": "bahubaliPoster"}   | movies/UpdateMovie/1   | 400        | invalid Arguments                                    |
            | {"name":"","yearOfRelease": 2021,"plot":"bahubali_plot","actorIds": [1,2],"genreIds": [3,4],"producedBy": 1,"coverImage": "bahubaliPoster"}        | movies/UpdateMovie/1   | 400        | invalid Arguments                                    |
            | {"name":"Bahubali","yearOfRelease":2021,"plot":"Bahubali_plot","actorIds": [1,5],"genreIds": [3,4],"producedBy": 1,"coverImage": "bahubaliPoster"}              | movies/UpdateMovie/1   | 400        | Actorids/generids cannot be found , enter valid data |
            | {"name":"Bahubali","yearOfRelease":2021,"plot":"Bahubali_plot","actorIds": [1,2],"genreIds": [3,6],"producedBy": 1,"coverImage": "bahubaliposter"} | movies/UpdateMovie/1   | 400        | Actorids/generids cannot be found , enter valid data |
            | {"name":"Bahubali","yearOfRelease":2021,"plot":"Bahubali_plot","actorIds": [1,2],"genreIds": [3,4],"producedBy": 6,"coverImage": "bahubaliposter"} | movies/UpdateMovie/1   | 400        | Producer id couldn't be found                        |
            | {"name":"Bahubali","yearOfRelease":2021,"plot":"Bahubali_plot","actorIds":[1,2],"genreIds":[3,4],"producedBy":1,"coverImage":"bahubaliPoster"}     | movies/UpdateMovie/-1  | 400        | invalid Arguments                                    |
            | {"name":"Bahubali","yearOfRelease":2021,"plot":"Bahubali_plot","actorIds":[1,2],"genreIds":[3,4],"producedBy":1,"coverImage":"bahubaliPoster"}     | movies/UpdateMovie/100 | 404        | No Content Found                                     |