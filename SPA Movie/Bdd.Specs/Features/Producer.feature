Feature: producer

@addproducer
Scenario: Adding an producer
    Given I am a client
    When I am making a post request to '<resourceEndpoint>' with the following Data '<Data>'
    Then response code must be '<statusCode>'
    And response data must look like '<responseData>'
    Examples: 
    | Data                                                                                           | resourceEndpoint      | statusCode | responseData      |
    | { "name":"jerry christopher","bio": "JerryBio","dob": "2001-12-02T00:00:00","gender": "male" } | producers/AddProducer | 201        | 1                 |
    | {"name":"","bio":"JerryBio","dob":"2001-12-02T00:00:00","gender":"male"}                       | producers/AddProducer | 400        | invalid Arguments |
    | {"name":"jerry christopher","bio":"","dob":"2001-12-02T00:00:00","gender":"male"}              | producers/AddProducer | 400        | invalid Arguments |
    | {"name":"jerry christopher","bio":"JerryBio","dob":"2001-12-02T00:00:00","gender":""}          | producers/AddProducer | 400        | invalid Arguments |
                                                                                          
                                                                                         
@displayproducers
Scenario:Display the list of producers
         Given I am a client
         When I make GET Request '<resourceEndpoint>' 
         Then  response code must be '<statusCode>'
         And response data must look like '<responseData>'
         Examples:
         | resourceEndpoint          | statusCode | responseData                                                                                                                                                                                    |
         | producers/GetAllProducers | 200        | [{"id":1,"name":"jerry Christopher","bio":"jerryBio","dob":"2001-12-02T00:00:00","gender":"male"},{"id":2,"name":"Priyanka","bio":"PriyankaBio","dob":"1998-06-05T00:00:00","gender":"female"}] |
       
       
@displayproducer
Scenario:Display the producer by using id
         Given I am a client
         When I make GET Request using Id '<resourceEndpoint>'
         Then  response code must be '<statusCode>'
         And response data must look like '<responseData>'
         Examples:
          | resourceEndpoint              | statusCode | responseData                                                                                          |
          | producers/GetProducerById/1   | 200        | {"id":1,"name":"jerry Christopher","bio":"jerryBio","dob":"2001-12-02T00:00:00","gender":"male"} |
          | producers/GetProducerById/100 | 404        | No Content Found                                                                                      |
          | producers/GetProducerById/-1  | 400        | invalid Arguments                                                                                     |
          | producers/GetProducerById/0   | 400        | invalid Arguments                                                                                     |

@deleteproducer
Scenario:Delete the producer using id
         Given I am a client
         When I make Delete Request '<resourceEndpoint>'
         Then  response code must be '<statusCode>'
         And response data must look like '<responseData>'
         Examples: 
            | resourceEndpoint             | statusCode | responseData         |
            | producers/DeleteProducer/1   | 200        | deleted successfully |
            | producers/DeleteProducer/-1  | 400        | invalid Arguments    |
            | producers/DeleteProducer/0   | 400        | invalid Arguments    |
            | producers/DeleteProducer/100 | 404        | No Content Found     |
          
         
@updateproducer
Scenario:update the producer using id
          Given I am a client
          When I make PUT Request '<resourceEndpoint>' with the following Data with the following Data '<Data>'
          Then  response code must be '<statusCode>'
          And response data must look like '<responseData>'
          Examples:     
            | Data                                                                                      | resourceEndpoint             | statusCode | responseData         |
            | {"name":"jerry Christopher","bio":"jerryBio","dob":"2001-12-02T00:00:00","gender":"male"} | producers/UpdateProducer/1   | 200        | updated successfully |
            | {"name":"","bio":"jerryBio","dob":"2001-12-02T00:00:00","gender":"male"}                  | producers/UpdateProducer/1   | 400        | invalid Arguments    |
            | {"name":"jerry Christopher","bio":"","dob":"2001-12-02T00:00:00","gender":"male"}         | producers/UpdateProducer/1   | 400        | invalid Arguments    |
            | {"name":"jerry Christopher","bio":"jerryBio","dob":"2001-12-02T00:00:00","gender":""}     | producers/UpdateProducer/1   | 400        | invalid Arguments    |
            | {"name":"jerry Christopher","bio":"jerryBio","dob":"2001-12-02T00:00:00","gender":"male"} | producers/UpdateProducer/-1  | 400        | invalid Arguments    |
            | {"name":"jerry Christopher","bio":"jerryBio","dob":"2001-12-02T00:00:00","gender":"male"} | producers/UpdateProducer/100 | 404        | No Content Found     |

 
         
   

