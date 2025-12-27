# AI-Supported Patient Tracking Platform (Lite)

A simple patient tracking application developed as a short case study.  
This project demonstrates a basic full-stack architecture using **Angular** and **.NET 8 Web API**, secured with **JWT authentication** and **Dockerized backend infrastructure**.

---

## Tech Stack

### Backend
- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Swagger (OpenAPI)
- Docker
- Docker Compose

### Frontend
- Angular 18
- TypeScript
- Reactive Forms
- Angular Router
- HttpClient

---

## Features

- JWT-based authentication and route protection
- Patient list and patient detail views
- Create and delete patient records
- Medical history display (sample seeded data)
- Mock AI prediction result returned from backend
- Logout functionality

---

## Project Structure

- **Backend**  
  ASP.NET Core Web API with layered architecture (Controllers, Services, EF Core)  
  Fully dockerized with PostgreSQL via Docker Compose

- **Frontend**  
  Angular application with feature-based structure, route guards, and HTTP interceptors

---

## Running the Project

The backend API and PostgreSQL database are orchestrated using **Docker Compose**.  
The Angular frontend runs separately on the host machine.

---

## Prerequisites

Make sure the following tools are installed:

- **Docker Desktop**
- **Node.js & npm**
- **Angular CLI**
  ```bash
  npm install -g @angular/cli


docker compose up --build -d*   The API will be accessible at **`http://localhost:5150`**.
*   Swagger UI will be available at **`http://localhost:5150/swagger`**.
npm install
ng serve*   The Angular application will be accessible at **`http://localhost:4200`**.

## Testing

Basic unit tests are included for the AI Prediction controller.

```bash
dotnet test


Frontend `http://localhost:4200` 
{
  "username": "doctor1",
  "firstName": "Ali",
  "lastName": "Başkan",
  "dateOfBirth": "1980-06-15",
  "password": "password123"
}
— Aslınur Şevval Sezgin  
Thank you for reviewing this project.
 :=) 
