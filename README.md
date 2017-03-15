# WikiCore - Work in Progress [![Build Status](https://travis-ci.org/philphilphil/WikiCore.svg?branch=master)](https://travis-ci.org/philphilphil/WikiCore)
Simple Wiki written in .NET Core MVC


## Setup
Install [.NET Core CLI](https://www.microsoft.com/net/core#windowsvs2015) for your OS.
Restore the packages used, generate Database (SQLite) with EntityFramework and run.
``` 
dotnet restore 
dotnet ef database update 
dotnet run
