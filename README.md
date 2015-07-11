# SemanticCodeGen
Code generation libraries for .Net using structured data as project starting point.

## Introduction
The motivation for creating this project is to find a way to leverage the power of JSON-LD and web vocabularies in order to be able to build entire
application suites based solely on project descriptions delivered in the form of structured data types like JSON-LD or RDFa.

### Phase I

1. Get structured input for a given vocabulary.
2. Translate structured input to json-ld
3. Create mapping from json-ld to C# types
4. Translate from vocabulary to C# code using Microsoft's System.CodeDom
5. Generate entities / interface / etc.

### Phase II

1. Create standard set of project templates for:	
	- ADO.NET Data Access Layer using EF CodeFirst
2. Use output from phase I as CodeFirst Migrations input to data access layer

### Phase III

1. Create standard set of project templates for:
	- ODataV4 WebAPI
	- ASP.Net MVC 5
2. Use ADO.NET Data Access Layer from Phase II
3. In Mvc, Scaffold Entity Views / Controllers that support the following functions for each Entity:
			- (B)rowse
			- (R)ead
			- (E)dit
			- (A)dd
			- (D)elete
			- (S)earch
			- (L)ist
4. In MVC Views, add application/json+ld and microdata to every view generated and update with data
5. In ODataV4, Create ODataConfig, EntitySet Controllers, etc. from Data Access Layer Phase II component

### Phase IV

1. Add OAuth 2 Support / Integration for all created components

### Final

1. Create looser coupling between components to allow support of wider range of frameworks, persistence, etc.
2. Add ability to choose which actions are included / not included in created code by leveraging JSON-LD Actions or DOAP or something
3. Expose Service API via structured data to aid in discoverability 

The general apporach will be to user another third party tool (jsonld.js)