Betting Game API

Overview

A simple REST API built with .NET 8.0 and C# that allows players to place bets on a randomly generated number.

Technologies Used

.NET 8.0 (ASP.NET Core Web API)

Entity Framework Core

SQL Server

xUnit & Moq (for Unit Testing)

How to Run the Project

1. Clone the Repository

git clone https://github.com/swapnalithk/BettingGameAPI
cd betting-game-api

2. Configure Database

Update appsettings.json with your SQL Server connection string.

3. Apply Database Migrations

dotnet ef migrations add InitialCreate
dotnet ef database update

4. Run the API

dotnet run

API will be available at: http://localhost:5043/api/game

API Endpoints

Register a Player

POST /api/game/register

{
  "username": "player1"
}

Place a Bet

POST /api/game/place-bet

{
  "playerId": 1,
  "points": 100,
  "predictedNumber": 3
}

Get Bet History

GET /api/game/bet-history/{playerId}

Running Unit Tests

dotnet test

