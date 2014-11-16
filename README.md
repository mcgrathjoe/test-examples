# test-examples

[![Build status](https://ci.appveyor.com/api/projects/status/ie66hbo1khyqmr93)](https://ci.appveyor.com/project/mcgrathjoe/test-examples)

An example project to illustrate some different types of automated testing

1. Created an empty ASP.Net Web Application called TestExamples.Web

2. Created class library projects for the following types of test
 
### TestExamples.Tests.Integration.nUnit
nUnit integration tests that test a data access component that reads from a SQL Database.
The tests reset the data in the database on each test run.
The tests don't currently set up the DB schema. 
It's assumed that there is a local DB based on the database project TestExamples.Database
These would run on every build.

### TestExamples.Muppets.Api.Test.Unit.HittingApi.nUnit
Spins up an in-memory HttpServer and makes http requests against specific resources.
This means the API routing is also covered by these tests.
To test via the API, rather than directly via the controller, we need to pass the in-memory 'stub' data store into the controller via the TestHttpServer setup - IOC?
These would run on every build.

### TestExamples.Muppets.Api.Test.Unit.HittingControllers.nUnit
Creates instances of Web Api Controllers, injects a stub in-memory data store into them, and runs controller methods directly.
These would run on every build.

### TestExamples.Tests.Unit.HittingControllers.nUnit
nUnit Unit tests that hit controllers and stub data access
These would run on every build.


## Other types of test to try out

* Specflow End-to-end smoke tests that test the whole application (run on every deployment to every environment)
* Specflow tests that hit controllers and stub data access? (run on every build) ?
* Specflow tests that hit UI and stub data access? (run on every deployment?) ?
* nUnit tests that hit service layer and stub data access? (run on every build) ?
* JavaScript tests
* UI screenshot tests
* Performance tests

## Note

* If you're using ReSharper you may need to set this option to true, so that the different unit test projects can load their own individual config files: 
ReSharper -> Options... -> Tools -> Unit Testing -> Use separate AppDomain for each assembly with tests'