test-examples
=============

An example project to illustrate some different types of automated testing

1. Created an empty ASP.Net Web Application called TestExamples.Web

2. Created class library projects for different types of test
  
	a) Specflow End-to-end smoke tests that test the whole application (run on every deployment to every environment)
  
	b) nUnit Integration tests for data access only (run on every build)
	
	c) nUnit Unit tests that hit controllers and stub data access (run on every build)
  
	d) Specflow tests that hit controllers and stub data access? (run on every build) ?
  
	e) Specflow tests that hit UI and stub data access? (run on every deployment?) ?
  
	f) nUnit tests that hit service layer and stub data access? (run on every build) ?
  
	g) JavaScript tests
  
	h) UI screenshot tests

  	i) Performance tests
