# Preview

![Screenshot 2024-06-10 202827](https://github.com/Kamelen123/Booking-Labb4/assets/145432557/8b68295b-8591-4c98-b4ef-253f2e018191)

# Bokningssystem
This application is a Booking System designed to manage customer appointments for companies. It includes functionalities to handle customer data, appointment scheduling, and company information. The system provides a REST API for interacting with the data, supporting operations such as creating, updating, deleting, and retrieving customer and appointment information. The API is built to be extensible and secure, ensuring data integrity and protection.

## Technologies Used

- **.NET 6**: The framework used to develop the application.
- **Entity Framework Core**: For database operations.
- **ASP.NET Core Web API**: To create the REST API.
- **SQL Server**: Database to store data.
- **Swagger**: For testing the API.
- **Repository Pattern**: For abstraction of data access logic.
- **AutoMapper**: For mapping between data models and DTOs.

## Project Structure

The solution consists of two projects:

1. **Model Class Library**: Contains all the data models.
2. **REST API**: The main project which references the Model Class Library and contains the API controllers and services.

## Database Models

The project includes three main models:

1. **Customer**: Contains customer details.
2. **Appointment**: Contains appointment details.
3. **Company**: Contains company details.

## Database and Repository Pattern

The database is built using Entity Framework Core, which helps in managing the database operations. We use the Repository Pattern to abstract the data access logic, which makes it easier to switch to a different data source in the future if needed.

## Custom Converters for DateOnly and TimeOnly

To handle the `DateOnly` and `TimeOnly` data types, custom converters were created to manage their serialization and deserialization. This ensures that date and time data are accurately processed in the CRUD operations.

### Custom Converters

- **DateOnlyConverter**: Handles the conversion of `DateOnly` type to and from JSON.
- **TimeOnlyConverter**: Handles the conversion of `TimeOnly` type to and from JSON.

These custom converters extend the capabilities of the API to process and return date and time values in a user-friendly manner, ensuring compatibility with the frontend applications.

## Temporal Table for Tracking Changes

To track changes made to the `Appointment` table, I implemented SQL Server Temporal Tables. This allows the program to maintain a full history of data changes and perform queries to view the data as it existed at any point in time.

### Benefits of Using Temporal Tables

- **Change Tracking**: Automatic tracking of changes without the need for additional application logic.
- **History Querying**: Ability to query historical data for auditing and reporting purposes.
- **Simplified Maintenance**: Reduced complexity in maintaining a history of changes manually.

## Data Transfer Objects (DTOs)

DTOs (Data Transfer Objects) are used to transfer data between different layers of the application. They help to decouple the internal representation of data from the way it is exposed to the client.

### Why Use DTOs?

1. **Data Encapsulation**: DTOs encapsulate the data and ensure that only the necessary information is transferred between layers.
2. **Security**: By using DTOs, we can control what data is exposed to the client, protecting sensitive information.
3. **Performance**: DTOs can improve performance by reducing the amount of data sent over the network.
4. **Maintainability**: They make the codebase easier to maintain and evolve by providing a clear contract between different parts of the system.

DTOs are used in the API controllers to map the data from the models to the client and vice versa. This approach ensures a clean separation of concerns and enhances the overall architecture of the application.

## REST API

The REST API supports various operations to manage customers, appointments, and companies. It follows RESTful principles and supports filtering and sorting. Additionally, the PATCH method is used for partial updates, allowing clients to update specific fields without affecting the entire resource.

## Testing

Testing of the API was done using Swagger to ensure the API was running correctly. The following steps were taken to validate the functionality:

1. **Start the API**: Ensure the API is running by launching it from Visual Studio.
2. **Open Swagger**: Navigate to the Swagger UI, accessible after launching.
3. **Perform CRUD Operations**: Use the endpoints to perform various operations such as:
    - Creating, updating, and deleting customers.
    - Scheduling, updating, and canceling appointments.
    - Managing company information.
4. **Validate Responses**: Check the responses to ensure they meet the expected results.
5. **Test Filtering and Sorting**: Use query parameters to test sorting and filtering functionalities.
6. **Test Partial Updates**: Use the PATCH method to partially update resources and verify that only the specified fields are updated.
