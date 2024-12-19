## Required install to run / process
1. Download SQLite DB Browser
2. Ensure you have the latest .NET version (9.0 as of writing)
```
dotnet --version
```
3. Download entity framework packages
```
dotnet tool install --global dotnet-ef
```
4. Restore packages
```
dotnet restore
```
6. Create database in SQlite (You can use DB Browser (SQLite) to create db on local)
7. Update appsettings.json in SEP7.WebAPI to reflect your own DB Browser path.
```csharp
"ConnectionStrings": {
    "DefaultConnection": "Data Source=C:\\Users\\frebo\\SEP7.db;"
```
8. Update database to add tables
```
dotnet ef database update
```
9. Make sure https is trusted
```
dotnet dev-certs https --trust
```
10. Run API on https
```
dotnet run --launch-profile https
```
11. Run WebApp
```
dotnet run
```

## User details for testing
**Admin** <br>
`username: testadmin` <br>
`password: test123`

**Regular user** <br>
`username: testuser` <br>
`password: test123`


## CSV Files for data testing
1. [VOLA CSV EXTRACT - Type 2.csv](https://github.com/user-attachments/files/18174116/VOLA.CSV.EXTRACT.-.Type.2.csv)
2. [VOLA CSV EXTRACT - Type 4.csv](https://github.com/user-attachments/files/18174117/VOLA.CSV.EXTRACT.-.Type.4.csv)
3. [VOLA CSV EXTRACT - Type 3.csv](https://github.com/user-attachments/files/18174118/VOLA.CSV.EXTRACT.-.Type.3.csv)
