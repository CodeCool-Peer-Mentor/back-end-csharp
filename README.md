# Peer Mentoring App

The back-end repository of Codecool's IRL coding challenge's Peer Mentoring application.

## Technologies

- ASP.NET Core 3.1 WebAPI
- Entity Framework Core 3.1
  - ASP.NET Core Identity (3.1)
  - PostgreSQL (3.1) provider
- Google Auth

## Setup

Select project folder

``` sh
cd src
```

### PostgreSQL

- Define the secret values

``` sh
dotnet user-secrets init
dotnet user-secrets set "DB:HOST" "localhost"
dotnet user-secrets set "DB:NAME" "peermentor"
dotnet user-secrets set "DB:USERNAME" "postgres"
dotnet user-secrets set "DB:PASSWORD" ""
```

- Apply the migration scripts

``` sh
dotnet ef database update
```

### WebAPI

Start the webserver

``` sh
dotnet run
```
