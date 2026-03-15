# Online Shopping Store with Cart System

## Project Description

This project is a web-based online shopping store that includes a fully functional shopping cart system built using ASP.NET Core MVC.

The application allows users to:
* Register and log into their accounts
* Browse products by category
* Add or remove items from their cart
* Manage item quantities
* Checkout or cancel purchases
* View their cart history

The system calculates total prices dynamically and stores all cart transactions in the database.

This project was originally developed on Windows using SQL Server, and was later successfully migrated and configured to run on macOS using MySQL and Pomelo Entity Framework Core, demonstrating cross-platform compatibility with .NET.

## Features

### Cart System

* Add items to the cart
* Remove items from the cart
* Increase or decrease item quantity
* Automatic calculation of total price
* Display individual item prices
* Display total purchase amount
* Clear cart functionality
* Checkout cart functionality
* Cancel Cart functionality
* Cart history showing previous purchases
* Cart records store:
  * Customer name
  * Date
  * Time
  * Total Amount

### User Features

* User registration and login
* Browse products by category
* View product details
* Access additional pages such as:

  * Home
  * More About Us
  * Customer Support
  * Privacy Policy
  * Order History

Note: Users must be logged in before adding items to their cart.

## Technologies Used

Backend

* C#
* ASP.NET Core MVC
* Pomelo EntityFrameworkCore MySQL Provider
* Entity Framework Core

Database

Originally: SQL Server (Windows development)
Current Version:
* MySQL
* MySQL Connector
* Pomelo EF Core MySQL provider

Frontend

* HTML
* CSS
* JavaScript
* Razor Views

Development Tools
* Visual Studio
* Visual Studio Code
* Git
* GitHub
* .NET CLI
* MySQL Server

## Cross-Platform Migration

### This project was initially developed on Windows using SQL Server.

### To run the application on macOS, the following changes were made:

Database Migration
* SQL Server replaced with MySQL
* Installed MySQL Server locally
* Updated connection string
* Added:
  * Pomelo.EntityFrameworkCore.MySQL

Entity Framework Changes
* Updated EF configuration to UseMySql() with Pomelo provider.

Environment SetUp
* .NET SDK
* dotnet-ef CLI tool
* MySQL Server
* VS Code

Migrations
Recreated migrations and rebuilt the database using:
* dotnet ef migrations add InitialCreate
* dotnet ef database update

## Project Structure 

* Controllers/
* Model/
* Views/
* Data/
* Migrations/
* wwwwroot/

Key Components:
* Models -> Database entities
* Controllers -> Application logic
* Views -> Razors UI pages
* Data -> Databasee context
* Migrations -> EF Core migrations

## Running the Application

1. Install dependencies
   * dotnet restore <name_of_YOUR_project_.csproj>
2. Run database migrations
   * dotnet ef database update
3. Run the application
   *  dotnet run or use hot reload by dotnet watch run
4. Application runs on:
   * hhtp://localhost:5220
  
## Screenshots


## Author

Glenda Patino Fulgidezza
