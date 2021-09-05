# CSVEDITOR
Web API created for editing .csv files in browser
App based on ASP.NET Core 
Hosts on IIS 
In app we are used :
MediatR
CsvHelper
ClosedXMl
EntityFramework
SQLServer
Bootstrap


There are two controllers 

AccountController which responsibility is Login and Register user both returns redirect to main page

CsvEditorController there are main functions
Get Transactions get transactions from context ,returns List<Transaction>
Import .csv file  add and replace data in context, returns List<Transaction>
Export takes export params which columns we whanna to see in excel file , returns .xslx file
Create takes data from transaction model  ,returns redirect to main page
Delete takes transaction id transaction ,returns redirect to main page
Edit takes transaction  returns, redirect to main page
Search takes Client name, returns List<Transaction>
Filter takes two strings(Status,Type) , returns List<Transaction>
All Redirects to main page are - return View("Index",viewModel)


All is working with Mediator commands and handlers

In Models we have 
CsvEditorContext which inherits IdentityUser
and contain Transaction DbSet
Transactions model
ViewModel containes 2 boolean ,transaction and List<Transaction>
first boolean is CreateMode when it's true on main page we going to see create window
second boolean is EditMode when it's true on main page we going to see edit window
Login and Register Data transfer object model
