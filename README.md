# Library Management System

A .NET API for managing books and their borrowing records.

## Project Overview

This Library Management System is a web API that allows users to access information about books and their associated borrowing records. The project is built using .NET 8.0 and follows clean architecture principles, separating concerns into multiple layers.

## Architecture

The solution is organized into four main projects:

1. **LibraryManagement.Core** - Contains domain entities, interfaces, and DTOs
2. **LibraryManagement.Database** - Handles data persistence with Entity Framework Core
3. **LibraryManagement.Infrastructure** - Implements services and business logic
4. **LibraryManagement.Api** - Exposes REST endpoints for client consumption

## Features

- 📚 Get all books with their borrowing records
- 🔍 Get a specific book with its borrowing records by ID
- 🌱 Automatic database seeding with sample books and borrowing records
- 📝 Swagger documentation

## Technologies Used

- 🔷 .NET 8.0
- 🗃️ Entity Framework Core 8.0
- 💾 SQL Server
- 📘 Swagger/OpenAPI

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server (Local or Express)
- Visual Studio 2022 or preferred IDE

### Database Setup

The application uses Entity Framework Core migrations to set up and seed the database:

1. The connection string is configured in `appsettings.json` to use a local SQL Server instance.
2. When the application first runs, it automatically applies any pending migrations and seeds the database with sample data.

### Running the Application

1. Clone the repository
2. Navigate to the solution folder
3. Build the solution:
4. Run the API:
5. The API will be available at:
- 🌐 HTTP: http://localhost:5000
- 🔒 HTTPS: https://localhost:5001

## API Endpoints

- **GET /api/books** - Retrieve all books with their borrowing records
- **GET /api/books/{id}** - Retrieve a specific book with its borrowing records by ID

## Project Structure

- **LibraryManagement.Core** 📌
- Entities (Book, BorrowingRecord)
- Interfaces (IBookRepository, IBookService)
- DTOs (BookDto, BorrowingRecordDto, BookWithBorrowingsDto)

- **LibraryManagement.Database** 💾
- ApplicationDbContext
- Repositories (BookRepository)
- Migrations
- DataSeeder

- **LibraryManagement.Infrastructure** ⚙️
- Services (BookService)

- **LibraryManagement.Api** 🌐
- Controllers (BooksController)
- Program.cs (Configuration and DI setup)
