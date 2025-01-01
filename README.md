# <span style="color: #4CAF50;">Ganz</span> Clean Architecture - DDD - Microservice

## <span style="color: #2196F3;">Overview</span>
This project is built on the principles of **Clean Architecture** with a **Domain-Driven Design (DDD)** approach. It is specifically designed to support **microservices** development, providing a robust and scalable structure for modern distributed systems.

---

## <span style="color: #FF5722;">Features</span>
- **Clean separation of concerns** with distinct layers.
- **Adherence to DDD principles** for domain modeling.
- **Designed for microservices**, promoting modularity and scalability.
- **Easy to maintain** and extend.

---

## <span style="color: #9C27B0;">Architecture Overview</span>

The solution follows the **Clean Architecture** structure:

### 1. <span style="color: #3F51B5;">Domain Layer</span>
   - Contains the **core business logic** and **entities**.
   - Free of **external dependencies**.

### 2. <span style="color: #3F51B5;">Application Layer</span>
   - Includes **use cases** and **application logic**.
   - Interacts with the **domain layer**.

### 3. <span style="color: #3F51B5;">Infrastructure Layer</span>
   - Handles **external concerns** like **databases**, **APIs**, and **message brokers**.

### 4. <span style="color: #3F51B5;">Presentation Layer</span>
   - Provides entry points such as **APIs** or **UI interfaces**.
