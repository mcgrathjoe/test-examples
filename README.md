# test-examples

[![Build status](https://ci.appveyor.com/api/projects/status/ie66hbo1khyqmr93)](https://ci.appveyor.com/project/mcgrathjoe/test-examples)

An example project to illustrate some different types of automated testing

1. Created an empty ASP.Net Web Application called TestExamples.Web

2. Created class library projects for the following types of test
 
* nUnit Integration tests for data access only (run on every build)
* nUnit Unit tests that hit controllers and stub data access (run on every build)


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