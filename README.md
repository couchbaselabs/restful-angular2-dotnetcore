# Couchbase, WebAPI, Angular 2, .NET Core Stack Example

A very basic example of a CEAN-type stack application that makes use of Couchbase Server's N1QL query language.

The full stack application separates the ASP.NET Core/WebAPI/Couchbase Server into the back-end and leaves Angular 2, HTML, and CSS as the front-end that requests data from the back-end and presents it to the user.

## Prerequisites

There are not many prerequisites required to build and run this project, but you'll need the following:

* [dotnet core 1.1](https://www.microsoft.com/net/core)
* Couchbase Server 4+
* Visual Studio Code, Visual Studio, or the text editor of your choice

## Installation & Configuration

Certain configuration in both the application and the database must be done before this project is usable.

### Application

Checkout the latest master branch from GitHub and open the project in Visual Studio Code.

Run `dotnet restore` to restore all the dependencies for .NET.
Run `npm install` to restore all the dependies for JavaScript
Run `dotnet publish` to build/compile the entire app.

### Database

This project requires Couchbase 4.0 or higher in order to function because it makes use of the N1QL query language.  With Couchbase Server installed, create a new bucket called **restful-sample** or whatever you've named it in your appsettings.json file.

In order to use N1QL queries in your application you must create a primary index on your bucket.  This can be done by using the Couchbase Query Client (CBQ).

On Windows, run the following to launch CBQ:

```
C:/Program Files/Couchbase/Server/bin/cbq.exe
```

With CBQ running, create an index like so:

```
CREATE PRIMARY INDEX ON `restful-sample` USING GSI;
```

Your database is now ready for use.

If you are using Couchbase 4.5, you can execute this query in the Query Workbench on Couchbase Console.

## Testing

Run `dotnet run` to execute the application.

Load the site in a browser: **http://localhost:5000**.

## Resources

Couchbase - [http://www.couchbase.com](http://www.couchbase.com)

Angular 2 - [https://angular.io/](https://angular.io/)

ASP.NET Core - [http://asp.net/core](http://asp.net/core)
