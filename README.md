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
