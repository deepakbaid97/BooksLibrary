## Setting the connection string to secret manager

````powershell
$sa_password = "[SA PASSWORD HERE]"
dotnet user-secrets set "ConnectionStrings:GameStoreContext" "Server=localhost; Database=GameStore; User Id=sa; Password=$sa_password;TrustServerCertificate=True"
```Read

``` macOS
export sa_password = "[SA PASSWORD HERE]"
dotnet user-secrets set "ConnectionStrings:BooksLibraryStoreContext" "Server=deepakserver.database.windows.net; Database=BooksLibrary; User Id=Deepakbaid; Password=$sa_password;TrustServerCertificate=True"
````
