# Application Architecture Description

## 1. Overview
- **Application Name:** Car Auction Management System
- **Description:** This is a test application designed to facilitate car auctions.

## 2. General Architecture
- **Architecture Type:** The application follows a "Clean Architecture" pattern, where all dependencies point to the application's Core.
- **Layers:** The application consists of an API that allows manipulation of elements related to car auctions. The layers are divided into:
  - **Contracts:** External representation of system information.
  - **Web API:** REST resources for system manipulation.
  - **Application:** A CQRS layer to isolate business logic Core from API resources.
  - **Domain:** Data processing and business rules.
  - **Data Persistence:** The Repository pattern is used to abstract data manipulation from other layers.

- **Dependency Injection:** All layers are accessed via dependency injection to facilitate testing and maintenance.

## 3. Data Flow
The "happy path" of the application works as follows:
1. The user registers vehicles to be auctioned. Each specific type of vehicle is registered at a specific endpoint.
2. Once the vehicles are registered, auctions can be created for each vehicle.
3. Auctions are created inactive, so they must be activated using the `start` endpoint.
4. Once the auction is active, bids can be placed on the auction.
5. The auction can be ended through the `end` endpoint.
6. If there is a winning bid, it is marked as the winner, and the vehicle becomes unavailable for future auctions.

## 4. Utilized Resources
- **Application:**
  - Swagger, MediatR, Fluent Validation, ErrorOr, Ardalis.SmartEnum, throw, SQLite
- **Testing:**
  - Xunit, FluentAssertions
