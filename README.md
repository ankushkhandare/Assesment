# .NET Sample API Take Home Interview
## Introduction
This project is meant for you to showcase your .NET skills and your personal style of implementing APIs.

In this project we are looking for:
- Your best practices
- Unit tests
- Clean code

We anticipate this to take under and hour.

## Solution Details
The solution contains two projects: a WebApi project and an `xUnit` test project to write your tests.

We have already scaffolded the project, however you are more than welcome to add/remove classes as you see fit.

## Requirements
In this take home interview you are tasked with: 
- Creating two endpoints in the `OrdersController`.
    - One to get recent orders.
    - One to submit new orders.

### Endpoints 
When you are complete we expect to be able to run the project and open the Swagger page to add and remove orders from the in memory database. They should:
- Validate parameters (discussed below)
- Return appropriate status codes for different cases
- Handle exceptions without crashing the application.

We would like to follow REST if possible.

### Database
The database for this project is implemented in-memory. There is one entity `Order`. It has been scaffolded.

An order should:
- Have a unique id 
- Have a entry date
- Have a description and name (each max length of 100)
- Have a flag denoting if the order was invoiced (defaults to true)
- Have a flag denoting if the order was deleted (defaults to false)

An order is considered "recent" if it is less than a day old and has not been deleted, users always want to see newest orders first.

### Configuration Issues
Assume a junior developer initialized this new API for you, resolve any issues with configuration needed for the API to work. 

### Unit Tests
- We expect to see unit tests written for all cases of public methods you write, including controllers.
- You are free to use any unit testing tools/libraries of your choosing: we have installed `xUnit`, `Moq`, and `FluentAsssertions` for you.

### Exceptions 
- Any exceptions generated/caught should be logged.

## Bonus
This is an optional bonus problem.

Build and endpoint (and corresponding, tests, classes, etc) that takes in a number of days and returns all orders that were submitted after today's date minus the number of days requested, excluding holidays and weekends.

For example, if the user gave the number of days as 5, but in that date range there is a weekend, we should be including orders from the next two days as well.