# .NET_CORE 2.0.0 + MYSQL FIX
In the current stable .net core version is a bugfix under linux systems.
So you have added the nuget packages manually:

## FIRST PLEASE INSERT IN YOU PROJECT.CSPROJ
* `<PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.0" />`
* `<PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="2.0.0-rtm-10062" />`

## SECOND UPDATE NUGET PACKAGES
Run in the the add package after adding in the projeckt folder:

* `dotnet add package MySql.Data.EntityFrameworkCore --version 8.0.8-dmr`
