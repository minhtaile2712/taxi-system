# Guide

## Prerequisites

- Docker Desktop with WSL
- NodeJs runtime
- Dotnet core SDK version 8

## Run infrastructure

From repository root, run this command:

```
docker compose up -d
```

## Start application

Run server:

```
cd .\taxi-backend\projects\TaxiSystem
dotnet ef database update
dotnet run -lp https
```

Run client app (for customers and drivers):

```
cd .\taxi-frontend\taxi-app-ts
npm run dev
```

## Using application

The client app run on: http://localhost:5173/

For convenience and being easy for functional testing, the app is running in location override mode. Location is copy from Google Maps, right click on the map to copy the `lat, long` number, then paste it to the input and hit `Send location`.

Make sure customers and drivers under test have location uploaded by using "Get current location in system" button.

The server API docs is at: https://localhost:7283/swagger/index.html
