# Library Management System API

A RESTful API built with ASP.NET Core for managing library operations, including books, customers, borrowing, and authentication. This project demonstrates secure, scalable, and efficient backend development practices.

## Features

- **User Management**:
  - Registration and Login with JWT-based authentication.
  - Role-based access control (Admin, User).

- **Book Management**:
  - CRUD operations for books.
  - Track book details such as title, author, publisher, and published date.

- **Customer Management**:
  - Manage customer profiles and their borrowing history.
  - Link customers with user accounts for secure access.

- **Borrowing System**:
  - Allow customers to borrow and return books.
  - Keep track of borrowing status and due dates.

- **Book Copies**:
  - Manage individual book copies to track their availability.
  - Monitor book statuses such as available, borrowed, or reserved.

## Technology Stack

- **Framework**: ASP.NET Core
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: JWT (JSON Web Token)
- **Version Control**: Git
- **Tools**: Visual Studio, Postman (for API testing)

## API Endpoints

### User Management
- `POST /api/Users/register`: Register a new user.
- `POST /api/Users/login`: Authenticate and log in a user.

### Book Management
- `GET /api/Books`: Retrieve all books.
- `GET /api/Books/{id}`: Retrieve a single book by ID.
- `POST /api/Books`: Add a new book.
- `PUT /api/Books/{id}`: Update a book's details.
- `DELETE /api/Books/{id}`: Delete a book.

### Customer Management
- `GET /api/Customers`: Retrieve all customers.
- `GET /api/Customers/{id}`: Retrieve a customer by ID.
- `PUT /api/Customers/{id}`: Update customer details.


### Borrowing
- `POST /api/Borrowings`: Get All Borrowing Records.
- `POST /api/Borrowings`: Borrow A Book.
- `POST /api/Borrowings/return`: Return a borrowed book.

### Book Copies
- `GET /api/BookCopies`: Retrieve all book copies.
- `POST /api/BookCopies`: Add a new book copy.
- `PUT /api/BookCopies`: Update A book copy.
- `DELETE /api/BookCopies`: Delete book copy.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/library-management-api.git
   cd library-management-api
