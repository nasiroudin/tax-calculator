# Tax Calculator App

## Description

A simple Tax Calculator that takes in a Postal Code and Annual Income to devise the calculated tax amount.
Each Postal Code has their own specific tax calculation namely:
<div align="center">

| Postal Code | Tax Calculation Type |
|-------------|----------------------|
| 7441        | Progressive          |
| A100        | Flat Value           |
| 7000        | Flat Rate            |
| 1000        | Progressive          |

</div>

Tax Calculator UI:

![image](assets/tax_calculator_app.png)

<div align="center">
  
[![Actions Status](https://github.com/nasiroudin/tax-calculator/workflows/Build/badge.svg)](https://github.com/nasiroudin/tax-calculator/actions)

</div>

## Development

Code has been written in C# using .NET8.
- IDE:  Rider or Visual Studio or VS Code
- Docker: For running application in containers
- Blazor: For UI Development
- NUnit: Writing Unit Tests

## Build
    $ dotnet build

## Tests
    $ dotnet test

## Run
### 1. Setup
#### 1.1 Using MSSQL Locally
Before running the program, you need to make sure you have a MSSQL Server running on your local machine or in docker containers.
If you plan to run an instance of MSSQL Server on your local machine, then you will need to restore the TAX_DB backup found in repository path:

    [docker/mssql/backup/TAX_DB.bak](https://github.com/nasiroudin/tax-calculator/blob/main/docker/mssql/backup/TAX_DB.bak)

#### 1.2 Using Docker
To setup MSSQL in docker along with other infra dependencies you can do:

    $ docker-compose -f .\docker-compose.infra.yml up -d

Note that the DB will be automatically created with its tables and sample data.

### 2. Run application using IDE
Steps:
- Set the correct connection string for MSSQL Server in [appsettings.json](https://github.com/nasiroudin/tax-calculator/blob/ca92eb9dda49bc6c9cc95fa5b7113c209f44eade/src/TaxCalculator.Api/appsettings.json#L10) in config ConnectionStrings:SqlServer 
- Run TaxCalculator.Api to start serving the API on port 5000.
- Run TaxCalculator.WebApp to start the Tax Calculator Web UI on port 5001

### 3. Run everything in docker

    $ docker-compose -f .\docker-compose.infra.yml up -d
    $ docker-compose -f .\docker-compose.apps.yml up -d

Once the application are up and running, open http://localhost:5001 on your browser which will redirect you to the logic screen.
The default login credentials is Username: admin and Password: admin

## Architecture
- Vertical Slice Architecture

<div align="center">

![image](assets/vertical_slice.png)

</div>
Reference: https://www.jimmybogard.com/vertical-slice-architecture/

- Application Overview
<div align="center">

![image](assets/application_arch.png)

</div>

## Feature Highlights
- Used Ant Design (https://antblazor.com) for Rapid UI Development along with the Blazor Framework
- Validation using Fluent Validation
- Dead Simple Authentication Middleware using Bearer Token for securing API
- Used LocalStorageManager in Blazor to store Bearer Token to interact with API
- Native .Net Dependency Injection
- Logging framework using Serilog and Seq (https://datalust.co/seq)
- Implemented Strategy and Factory Patterns to design the Tax Calculation
- Used Dapper as ORM
- Testing using NSubstitute and NUnit
- InMemory Cache to cache configuration data

## Endpoints

### Login
    Method: POST 
    Url: http://localhost:5000/api/user/login
    Content-Type: application/json
    Body:
    {
        "username": "admin",
        "password": "admin"
    }

### Calculate Tax
    Method: POST
    Url: http://localhost:5000/api/tax/calculate
    Content-Type: application/json
    Authorization: Bearer CfDJ8OXsE_ZUlW1AokKx4C9gm7dgWWoTSz6Ml2jJB9j6sxUxyq-HgLcks4O-AnSIk7cA-VYQfB0uJRcpulZv1na380IJs5q_qP1Iapqd2CGv9CBq1m7xqL3d9lNbiU5zVJ63KcsdMpZ0OdfMtw04sdfdmxOqHAVUAyt_iZSsAtuupO1hVJRvmxt1SsdvdXI8-qQsSaoXhlE8M1FBsNeYKkVFF7nx1xlvVq1g6QokR2vYqbhZX2QgzcKtZgbWQ3QAiHxXDi-X-YzII5rId2k6kpRe90E
    Body:
    {
        "postal_code": "7441",
        "annual_income": 2000
    }

### Get Tax Details
    Method: GET
    Url: http://localhost:5000/api/tax/details
    Content-Type: application/json
    Authorization: Bearer CfDJ8OXsE_ZUlW1AokKx4C9gm7dgWWoTSz6Ml2jJB9j6sxUxyq-HgLcks4O-AnSIk7cA-VYQfB0uJRcpulZv1na380IJs5q_qP1Iapqd2CGv9CBq1m7xqL3d9lNbiU5zVJ63KcsdMpZ0OdfMtw04sdfdmxOqHAVUAyt_iZSsAtuupO1hVJRvmxt1SsdvdXI8-qQsSaoXhlE8M1FBsNeYKkVFF7nx1xlvVq1g6QokR2vYqbhZX2QgzcKtZgbWQ3QAiHxXDi-X-YzII5rId2k6kpRe90E
