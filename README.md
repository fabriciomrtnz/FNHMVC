FNHMVC
======
FNHMVC is an architectural foundation for building maintainable web applications with ASP.NET, MVC, NHibernate & Autofac.

### Features:
* CRUD demo in ASP.NET MVC with NHibernate
* Generic repository for NHibernate
* Repository Pattern and Unit of Work-Dependency Injection using Autofac
* Mobile specific pages using JQuery Mobile
* ASP.NET MVC Area for Mobile specific pages
* Using NHibernate with custom Membership/Role provider
* Using Fluent NHibernate for mappings in strongly typed C# code
* Unit test samples using Autofac
* Unit test samples using Selenium WebDrive (user navigation simulation)
* Windows Azure Web Role

### Architectural & Design Patterns:
* Model-View-Controller with ASP.NET MVC
* Object-relational mapping with NHibernate
* Dependency Injection with Autofac
* Domain Driven Design
* Command Query Responsibility Separation
* Repository pattern
* Unit of work pattern
* And much more..

--------------------------------------------------------------------------------------------------

### Version 2.0.1

### Technologies:
* Windows Azure SDK 2.3
* ASP.NET MVC 5.1.2
* ASP.NET Web API 5.1.2
* NHibernate 3.3.3.4001
* Fluent NHibernate 1.4
* Autofac 3.5
* Automapper 3.2.1
* Selenium WebDriver 2.42

### Requirements:
* Visual Studio 2013
* MSSQL Express (NHibernate can be easily configured to use all the mayor SQL servers)

### Notes
* MVC authentication and authorization system has been migrated from Microsoft Membership to Microsoft Identity.
* Automapper has been implemented for domain->command, command->domain and domain -> dto objects conversions. dto == Data Transfer Objects.
* Command objects have been moved to the Model project to allow the use of Automapper.
* Web API sample project with unit test added. Including the use of DTOs.
* Nuget packages not included in download zip. Please make sure you configure VS to download them for you

### Version 2.0

### Technologies:
* Windows Azure SDK 1.8 October 12 Release
* ASP.NET MVC 4 RTM
* ASP.NET Web API
* NHibernate 3.3
* Fluent NHibernate 1.4
* Autofac 3.0.1
* Selenium WebDriver 2.24

### Requirements:
* Visual Studio 2012
* MSSQL Express (NHibernate can be easily configured to use all the mayor SQL servers)

### Version 1.0

### Technologies:
* Windows Azure SDK 1.6
* ASP.NET MVC 4 RC
* ASP.NET Web API
* NHibernate 3.2
* Fluent NHibernate 1.3.0
* Autofac 2.6.1

### Requirements:
* Visual Studio 2010
* MSSQL Express (NHibernate can be easily configured to use all the mayor SQL servers)


--------------------------------------------------------------------------------------------------

### Case Studies:
* http://www.iseguro.com

### Roadmap
* Common.Logging - Logging solution
* Lucene.NET - NoSQL fast text search solution.
* ActionMailer.NET - MVC Views style mailing solution.
