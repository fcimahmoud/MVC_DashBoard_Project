# Multi-Layered MVC Application

## Overview
This project is a modular **MVC-based application** designed using **Multi-Layer Architecture** (PL, BLL, DAL) to ensure scalability, maintainability, and separation of concerns. It includes a dynamic **Presentation Layer (PL)** with features for CRUD operations and a robust backend to manage business logic and data access efficiently.

The application implements key design patterns like **Generic Repository** and **Unit of Work** to ensure flexibility and reusability. It also integrates **Entity Framework Core** and **SQL Server** for database management and **ASP.NET Identity** for secure user authentication and authorization.

---

## Features
- **User Management:** Role-based access control using **ASP.NET Identity**.
- **CRUD Operations:** Dynamic views for creating, editing, deleting, and viewing records.
- **Responsive UI:** Built with **Bootstrap** for a modern and user-friendly interface.
- **Validation:** Server-side validation using **Data Annotations** and client-side validation with **jQuery Validation**.
- **Scalable Design:** Designed with best practices in architecture and patterns for extensibility.
- **Database Integration:** Efficient ORM with **Entity Framework Core** and **SQL Server**.

---

## Technologies Used
- **Frontend:** Razor Pages, Bootstrap, jQuery
- **Backend:** ASP.NET Core, C#, LINQ
- **Database:** SQL Server, EF Core
- **Design Patterns:** Repository Pattern, Unit of Work
- **Architectures:** Multi-Layer Architecture
- **Authentication:** ASP.NET Identity
- **Tools:** Visual Studio, Git & GitHub

---

## Installation and Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/fcimahmoud/MVC_PROJECT.git
   cd MVC_PROJECT
2. Open the solution in Visual Studio.
3. Update the database connection string in appsettings.json under the Presentation Layer (PL):
   ```json
    "ConnectionStrings": {
        "SQLConnection": "Your_Connection_String"
    }
4. Apply the migrations to create the database schema:
    ```bash
    dotnet ef database update

5. Run the application:

- Press ```F5``` in Visual Studio or run ```dotnet run``` in the terminal.

---

## Folder Structure
- **Presentation Layer (PL):** Handles user interface and input/output operations.
  - **Controllers:** Includes controllers like `AccountController`, `DepartmentsController`, `EmployeesController`, and more.
  - **Views:** Dynamic views for CRUD operations with reusable partials such as `CreateEditPartial`.
  - **ViewModels:** Provides abstraction for data sent between PL and BLL.

- **Business Logic Layer (BLL):** Implements business logic and rules using services.

- **Data Access Layer (DAL):** Manages database connections and operations via **Entity Framework Core**.

---

## Contribution Guidelines
1. Fork the repository and create a new branch for your feature.
2. Submit a pull request with a detailed description of the changes.
3. Follow coding standards and ensure the application builds without errors.

---

## License
This project is licensed under the MIT License.

---

## Contact
For any questions or feedback, feel free to contact:
- **Email:** [ma5740@fayoum.edu.eg]
- **GitHub:** [github.com/fcimahmoud](https://github.com/fcimahmoud)

---
