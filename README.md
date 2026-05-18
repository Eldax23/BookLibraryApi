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

---

## ⚙️ Installation

### Prerequisites

Make sure you have the following installed before proceeding:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express)
- [Git](https://git-scm.com/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

---

### 1. Clone the Repository

```bash
git clone https://github.com/Eldax23/BookLibraryApi.git
cd BookLibraryApi
```

---

### 2. Configure the Database

Open `appsettings.json` and update the connection string to match your SQL Server setup:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> If you're using SQL Server Express, your server name is typically `.\SQLEXPRESS`.

---

### 3. Configure JWT Settings

In `appsettings.json`, set your JWT secret key and token settings:

```json
"JWT": {
  "Key": "your_super_secret_key_here",
  "Issuer": "LibraryManagementAPI",
  "Audience": "LibraryManagementClient",
  "DurationInDays": 7
}
```

> ⚠️ Make sure the `Key` is at least 32 characters long for proper signing.

---

### 4. Restore Dependencies

```bash
dotnet restore
```

---

### 5. Apply Database Migrations

```bash
dotnet ef database update
```

> If the `Migrations` folder doesn't exist yet, run this first:
> ```bash
> dotnet ef migrations add InitialCreate
> ```

---

### 6. Run the Application

```bash
dotnet run
```

The API will start on `https://localhost:{port}` by default.

---

### 7. Test the API

Navigate to the Swagger UI in your browser to explore and test all endpoints:

```
https://localhost:{port}/swagger
```

Alternatively, import the API into **Postman** and start making requests.

---

## 📄 License

This project is licensed under the MIT License.
