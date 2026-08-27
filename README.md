# Campus Equipment Borrowing System - Architecture

## 1. Solution Structure
* **Domain:** Contains the important concepts, rules, and states belonging to the problem itself, such as `Student`, `Equipment`, and `Borrowing`.
* **Application:** Contains operations and use cases performed by the application. It coordinates domain objects and uses interfaces to abstract external dependencies.
* **Infrastructure:** Contains implementations concerned with external technical mechanisms. For this lab, it holds the simple in-memory storage collections.
* **Tests:** Contains automated tests for application or domain behavior (currently a placeholder for future test projects).

## 2. Dependency Direction
The dependencies flow inward toward the core business logic. The external layers (Console/UI and Infrastructure) depend on the Application layer, which in turn depends solely on the Domain layer[cite: 1]. 

```text
 Executable / Console
           │
           ▼
      Application
        │      ▲
        ▼      │
      Domain   │
               │
      Infrastructure