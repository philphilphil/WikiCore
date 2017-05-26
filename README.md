# WikiCore [![Build Status](https://travis-ci.org/philphilphil/WikiCore.svg?branch=master)](https://travis-ci.org/philphilphil/WikiCore)
WikiCore is a modest, small and fast Wiki featuring [MarkDown](https://daringfireball.net/projects/markdown/) editing.
Unlike regular Wikis pages are organized with tags instead of categories. 
Although there is a Login-System, there is no account-management neither user information bound to pages. The Intended purpose is private use.

In the default settings its possible to create only one account. Please see [the wiki](https://github.com/philphilphil/WikiCore/wiki/Configuration) for more information.

Latest Stable release: [v1.1](https://github.com/philphilphil/WikiCore/releases)
![WikiCoreScreenshot](http://i.imgur.com/KuK1Twi.png "Edit view")
## Setup
### Deploy on a Linux Server
1. Install [.NET Core Runtime](https://www.microsoft.com/net/download/core#/runtime)
2. Download latest WikiCore-Version from the [Releases-page](https://github.com/philphilphil/WikiCore/releases)
3. Unpack, cd into the directory and run: dotnet WikiCore.dll

It will run on http://localhost:5000. Configure your webserver (e.g. apache) as reverse proxy.
There is also a guide from Microsoft: [Publish to a Linux Production Environment](https://docs.microsoft.com/en-us/aspnet/core/publishing/linuxproduction).
### Run local for Development
Install [.NET Core CLI](https://www.microsoft.com/net/core#windowsvs2015) for your OS.
Pull the repository, restore the packages, generate Database (SQLite) with EntityFramework and run.
 
    dotnet restore 
    dotnet ef database update 
    dotnet run
### Docker
Publish (builds a clean version) and build Container
    
    dotnet publish
    sudo docker build -t wikicore .
## Tests
To run the tests simply run

    dotnet test
## Bugs
If you find any Bugs please open a Ticket or send me a Mail
