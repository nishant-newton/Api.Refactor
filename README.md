Approach:
1. To avoid using any third party libraries
2. To avoid using boiler plate and ready to use code
3. To write pure code as much as possible
4. To showcase the .NET capability. Few things being implemented at some place and not at other place. This has been done intentionally
5. DRY
6. SOLID principle

API has been designed considering below principles
1. SoC
2. Layered
3. Abstraction
4. Testability and Mockability
5.  KISS (Keep it simple, stupid)


Things to note:
1. Implemented the good old ADO.NET abstraction layer instead of any other ORM. Reason for this is to showcase the understanding of SQL and integration of .NET with SQL. With ORM's this fun was out. Very easy to plug in EF Core or any other ORM if required. Nothing is my favourite it all depends on the application complexity from data cruding perspective.

2. For global error handling ErrorController is the class. Having a separate class for all exceptions gives the flexibility to take multiple actions on the error and based on the environment. For eg: this class can send notifications if exception occurs in production environment.

3. ValidationFilterAttribute class is for validating input and model state. This attribute has not been put in all the action intentionally for comparison reasons.

4. ValidateEntityExistsAttribute checks for the presence of Id in Products table in the persistent storage. Again intentionally not being implemented at every required method for comparison reasons.

5. DTO and POCO entities have been used

6. Concrete classes for SQlite handling have been used to achieve asynchronous behavior. For eg: Used SQliteConnection instead of IDbconnection

7. For demonstration purpose console logging has been implemented and that would log only exceptions. If the api would run inside the docker container then console logging could be exposed to a log shipper for forwarding and centralizing the log content.

8. Dockerfile is present for demonstration purpose

What is missing:

1. Full test coverage is missing due to lack of time
2. Unit test have been implemented
3. Integration test has been implemented
4. Comparison of json response with json schema (ref: products-json-schema.json file Api.IntegrationTest project)
4. Authentication


