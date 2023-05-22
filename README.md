# AE-Shipping



## Getting started
The goal of this challenge is to build a solution comprised of REST APIs that finds the closest port to a given ship and calculates the estimated arrival time based on velocity and geolocation (longitude and latitude) of given ship.

## Technologies 
- .NET 6
- CQRS Patten
- SQL Server
- Swagger
- NetTopologySuite

## First time setup
1. Clone project from git
2. Build the solution to restore the NuGet packages
3. Update connection string in `Shipping.API\appsetting.json` file
```
  "ConnectionStrings": {
    "AppConnection": "Server=127.0.0.1, 1433;Database=Shipping-System;User Id=sa;Password=Password@123;"
  },
```
4. The database migrator will auto run if the `Shipping-System` database is not existing or there's a missing migration. Seed data for `Port` and `Ship` will also auto generate in this migration
5. After the `Swagger` page show up, you are ready to go with all the API

## API Endpoint

### Ship
- `GET: api/ship` -> Get all ships in the system
- `GET: api/ship/{id}` -> Get ship by Id
- `PUT: api/ship/{id}` -> Update ship by Id
- `DELETE: api/ship/{id}` -> Delete ship by Id
- `POST: api/ship/delete` -> Delete multiple ships by list Ids
- `POST: api/ship` -> Create new ship

### Port
- `GET: api/port` -> Get all ports in the system
- `GET: api/port/{id}` -> Get port by Id
- `GET: api/port/closet/{shipId}` -> Get closet port by shipId

## Assumptions
- The velocity unit is `km/h`
- Distance return between 2 `NetTopologySuite Point`s unit is `kilometer`
- No authentication implemented
