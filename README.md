# Atria
A webservice for the organization of webservices

## Setup
0. Install .NET SDK
1. Install [postgreSQL](https://www.postgresql.org/) and configure it with Atria
    - Either set up a user with username `user` and password `password` that can login and create databases (easy GUI interface: [pgAdmin](https://www.pgadmin.org/), see [Setup Guide](#setup-database-for-atria-with-pgadmin) below)
    - Or change the `DatabaseString` in appsettings.json appropriately
2. Install Entity Framework Core (`dotnet tool install --global dotnet-ef`)
3. Initialize the database (`dotnet ef database update` in the `Backend` directory of the repository)
4. Start both backend and frontend in their corresponding repository (e.g. with `dotnet watch`)
5. Optional: Create mock data with `DatabaseMocker`

## Setup Database for Atria with pgAdmin
0. Install [pgAdmin](https://www.pgadmin.org/)
1. In pgAdmin 4: Servers -> Login/Group Roles -> Right Click -> Create -> Login/Group Role
2. 1. General ->  Name: `user`
   2. Definition -> Password: `password`
   3. Privileges -> Can login?: `true`, Create databases?: `true`