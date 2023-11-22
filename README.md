## API Documentation

### Health Controller

#### Check Health

Endpoint: `GET /api/Health`

Check the health status of the API.

**Response:**
- `200 OK`: Returns the current date and time as a health check response.
- `500 Internal Server Error`: If there is a server error.

### Car Controller

#### GetAll Cars

Endpoint: `GET /api/Car`

Get a list of cars based on the provided filter.

**Parameters:**
- `CarFilter` (Query Parameter): Filter to narrow down the list of cars.

**Response:**
- `200 OK`: Returns a list of cars.
- `400 Bad Request`: If there is a validation error or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

#### Get Car by Id

Endpoint: `GET /api/Car/{id}`

Get a specific car by its ID.

**Parameters:**
- `id` (Path Parameter): ID of the car.

**Response:**
- `200 OK`: Returns the details of the requested car.
- `400 Bad Request`: If the car ID is invalid or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

#### Create Car

Endpoint: `POST /api/Car`

Create a new car.

**Parameters:**
- `CarDTO` (Request Body): Details of the car to be created.

**Response:**
- `200 OK`: Returns the details of the created car.
- `400 Bad Request`: If there is a validation error or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

#### Update Car

Endpoint: `PUT /api/Car`

Update an existing car.

**Parameters:**
- `CarDTO` (Request Body): Details of the car to be updated.

**Response:**
- `200 OK`: Returns the details of the updated car.
- `400 Bad Request`: If there is a validation error, the car ID is invalid, or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

#### Delete Car

Endpoint: `DELETE /api/Car/{id}`

Delete a car by its ID.

**Parameters:**
- `id` (Path Parameter): ID of the car to be deleted.

**Response:**
- `200 OK`: Returns the details of the deleted car.
- `400 Bad Request`: If the car ID is invalid or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

### Client Controller

#### GetAll Clients

Endpoint: `GET /api/Client`

Get a list of clients based on the provided filter.

**Parameters:**
- `ClientFilter` (Query Parameter): Filter to narrow down the list of clients.

**Response:**
- `200 OK`: Returns a list of clients.
- `400 Bad Request`: If there is a validation error or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

#### Get Client by Id

Endpoint: `GET /api/Client/{id}`

Get a specific client by their ID.

**Parameters:**
- `id` (Path Parameter): ID of the client.

**Response:**
- `200 OK`: Returns the details of the requested client.
- `400 Bad Request`: If the client ID is invalid or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

#### Create Client

Endpoint: `POST /api/Client`

Create a new client.

**Parameters:**
- `ClientDTO` (Request Body): Details of the client to be created.

**Response:**
- `200 OOK`: Returns the details of the created client.
- `400 Bad Request`: If there is a validation error or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

#### Update Client

Endpoint: `PUT /api/Client`

Update an existing client.

**Parameters:**
- `ClientDTO` (Request Body): Details of the client to be updated.

**Response:**
- `200 OK`: Returns the details of the updated client.
- `400 Bad Request`: If there is a validation error, the client ID is invalid, or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

#### Delete Client

Endpoint: `DELETE /api/Client/{id}`

Delete a client by their ID.

**Parameters:**
- `id` (Path Parameter): ID of the client to be deleted.

**Response:**
- `200 OK`: Returns the details of the deleted client.
- `400 Bad Request`: If the client ID is invalid or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

### Rent Controller

#### GetAll Rents

Endpoint: `GET /api/Rent`

Get a list of rents based on the provided filter.

**Parameters:**
- `RentFilter` (Query Parameter): Filter to narrow down the list of rents.

**Response:**
- `200 OK`: Returns a list of rents.
- `400 Bad Request`: If there is a validation error or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

#### Get Rent by Id

Endpoint: `GET /api/Rent/{id}`

Get details of a specific rent by its ID.

**Parameters:**
- `id` (Path Parameter): ID of the rent.

**Response:**
- `200 OK`: Returns the details of the requested rent.
- `400 Bad Request`: If the rent ID is invalid or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

#### Create Rent

Endpoint: `POST /api/Rent`

Create a new rent.

**Parameters:**
- `RentDTO` (Request Body): Details of the rent to be created.

**Response:**
- `200 OK`: Returns the details of the created rent.
- `400 Bad Request`: If there is a validation error or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

#### Update Rent

Endpoint: `PUT /api/Rent`

Update an existing rent.

**Parameters:**
- `RentDTO` (Request Body): Details of the rent to be updated.

**Response:**
- `200 OK`: Returns the details of the updated rent.
- `400 Bad Request`: If there is a validation error, the rent ID is invalid, or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

#### Delete Rent

Endpoint: `DELETE /api/Rent/{id}`

Delete a rent by its ID.

**Parameters:**
- `id` (Path Parameter): ID of the rent to be deleted.

**Response:**
- `200 OK`: Returns the details of the deleted rent.
- `400 Bad Request`: If the rent ID is invalid or a business rule is violated.
- `500 Internal Server Error`: If there is a server error.

---

### Business Rules

1. **Car Availability:**
   - Cars can only be rented if they are available.
   - A car is considered available if it is not currently rented or if its last operation was a return.

2. **Unique Client CPF:**
   - Each client must have a unique

 CPF in the system.

3. **Unique Car Brand and Model:**
   - Each car must have a unique combination of brand and model in the system.

4. **Client Age Validation:**
   - Clients must be of legal age to rent a car.

5. **Rent Operation Validation:**
   - Rent operations must be either "A" for rent or "D" for return.

6. **Client Existence Validation:**
   - Clients must exist in the system before creating or updating a rent.

7. **Car Existence Validation:**
   - Cars must exist in the system before creating or updating a rent.

8. **Rent Existence Validation:**
   - Rents must exist in the system before updating or deleting them.

9. **CPF Existence Validation:**
   - When creating or updating a client, the CPF must not already exist in the system.

10. **Client Deactivation Validation:**
   - Deactivated clients cannot be used in rent operations.

11. **Car Deactivation Validation:**
   - Deactivated cars cannot be rented.

12. **Rent Deactivation Validation:**
   - Deactivated rents cannot be updated or deleted.

---

**Note:** This documentation provides an overview of the API endpoints, their parameters, and expected responses. It also outlines key business rules to be considered for the proper functioning of the system. Make sure to implement these rules in the application logic to ensure data integrity and consistent behavior.