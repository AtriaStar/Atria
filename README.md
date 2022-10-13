# Atria
A webservice for the organization of webservices

## Setup
0. Install .NET SDK
1. Install [postgreSQL](https://www.postgresql.org/) and configure it with Atria
    - Either set up a user with username `user` and password `password` that can login and create databases (easy GUI interface: [pgAdmin](https://www.pgadmin.org/))
    - Or change the `DatabaseString` in appsettings.json appropriately
2. Install Entity Framework Core (`dotnet tool install --global dotnet-ef`)
3. Initialize the database (`dotnet ef database update` in the `Backend` directory of the repository)
