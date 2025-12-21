# Hybrid Caching Web API

This Web API application implements a hybrid caching mechanism that combines Redis (as a distributed cache) and in-memory caching (for fast local access) to enhance application performance and scalability. It is designed to efficiently manage data retrieval, minimize database hits, and provide a seamless experience for end-users.

## Key Features

- **Hybrid Caching**: Utilizes both Redis and in-memory caching to optimize data access times and reduce load on the database.
- **Scalability**: The use of Redis allows for distributed caching, making the application scalable across multiple instances.
- **Performance**: Local in-memory caching ensures rapid access to frequently requested data.
- **Asynchronous Operations**: Implements asynchronous programming patterns to improve responsiveness and resource utilization.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/hasandex/HybridCachingDemo.git

2. Install the required packages:
   ```bash
   dotnet restore
3. Configure your Redis connection string in appsettings.json:
   json
  {
    "ConnectionStrings": {
        "Redis": "your_redis_connection_string"
    }
  }


4. Run the application:
   ```bash
   dotnet run


## Usage
- Access the API endpoints through your web browser or API client (like Postman).
- The application supports retrieving product data, which will be cached in both Redis and in-memory for optimized performance.

