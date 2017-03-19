# WikiCore [![Build Status](https://travis-ci.org/philphilphil/WikiCore.svg?branch=master)](https://travis-ci.org/philphilphil/WikiCore)
WikiCore is a modest, small and fast Wiki featuring [MarkDown](https://daringfireball.net/projects/markdown/) editing.

Unlike regular Wikis pages are organized with tags.

Latest Stable release: [v1.0](https://github.com/philphilphil/WikiCore/releases)
![WikiCoreScreenshot](http://i.imgur.com/l0zdhWg.png)
## Setup
Install [.NET Core CLI](https://www.microsoft.com/net/core#windowsvs2015) for your OS.
Restore the packages used, generate Database (SQLite) with EntityFramework and run.
 
    dotnet restore 
    dotnet ef database update 
    dotnet run
  
## Tests
To run the tests simply run

    dotnet test
## Bugs
If you find any Bugs please open a Ticket or send me a Mail
