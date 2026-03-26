# BandScope
I carried out this project as part of my **IPA (Individuelle Praxisarbeit, translates to individual practical work)**, which served as the final exam for my apprenticeship.
## What is an IPA (Individuelle Praxisarbeit)?
In order to successfully complete the apprenticeship as an 'Informatiker Applikationsentwickler EFZ' in Switzerland and obtain the 'Eidgenössisches Fähigkeitszeugnis (EFZ)', an individual practical project must be carried out during the final year of the apprenticeship, which must be completed within 10 working days (80 hours). The evaluation of this project has a significant impact on the final grade on the EFZ.
## Project Overview
The goal of the project was to design and implement an independent, layered software solution from analysis and architecture through implementation and manual testing. The application combines local data management with external artist metadata from TheAudioDb and provides authenticated functionality such as reviews, favorites, and administrative user management.
> The project was built and tested for **localhost execution**.  
> Deployment to a public environment was **not part of the IPA requirements**.

BandScope allows users to:

- search artists by name, style, and genre
- browse a paginated artist list
- open a detailed artist page with metadata and discography
- register and log in with a local user account
- create, edit, and delete their own reviews
- add and remove favorite artists
- manage their own profile

Administrators can additionally:

- manage users
- delete reviews of other users
- remove favorites of other users

The application combines artists stored in the local database with artists loaded from the external TheAudioDb REST API.
## Technical Stack
### Frontend
- Blazor WebAssembly
- Radzen Blazor Components
- Blazored.LocalStorage
### Backend
- ASP.NET Core Web API
- JWT Authentication
- Serilog for logging
### Business / Validation
- FluentValidation
- BCrypt for password hashing
### Data Access
- Entity Framework Core
- Microsoft SQL Server
## High-level structure
The application follows a layered 3-tier architecture.
- **Client**  
  Blazor WebAssembly frontend responsible for UI, user interaction, local auth state, and API communication

- **Server**  
  ASP.NET Core backend exposing REST endpoints, handling authentication, authorization, external API access, and HTTP pipeline concerns

- **Logic**  
  Business logic and validation

- **DataAccess**  
  Entity Framework Core context, entity configuration, and CRUD access to the SQL Server database

- **Common**  
  Shared DTOs, models, enums, and common code used across multiple projects

## Core Features
### Artist functionality
- artist search
- artist filtering
- artist detail view
- optional enrichment of the local database with artists that are not available through the external API
- discography display
### User functionality
- registration
- login
- profile update
- profile deletion
- personal reviews
- favorites list
### Review functionality
- one review per user per artist
- edit existing reviews
- delete reviews
- rating + rich text review content
### Admin functionality
- user management
- review management across users
- favorites management across users
## Security and Error Handling
This project includes several security and robustness measures:

- passwords are not stored in plain text
- password verification is based on BCrypt
- authentication is implemented with JWT
- authorization is enforced in both frontend and backend
- global backend error handling is implemented through a GlobalExceptionFilter
- server-side logging is implemented with Serilog
- users receive only general error messages, while detailed errors are logged on the server side

In the original IPA context, the database was also secured with encryption measures in SQL Server (TDE).

---

## Local Development Setup
### Prerequisites
To run the project locally, you need:

- Visual Studio 2022
- .NET 8 SDK
- SQL Server 2022 (or a compatible local SQL Server instance)
- Optionally SQL Server Management Studio (SSMS)
- An API Key for the REST-API from TheAudioDb (They list a key for their free tier on their website)

Please note, that if you use the free api key from TheAudioDb, not all the features used in this project will work as intended.
### Getting Started
#### 1. Clone the repository
#### 2. Open the solution
Open the BandScope solution in Visual Studio.
#### 3. Restore NuGet packages
Restore packages through Visual Studio or build the solution once.
#### 4. Check server configuration
The server project contains an appsettings.json file with the required configuration for local development.

Important sections include:

- ConnectionStrings:DefaultConnection
- JwtSettings
- TheAudioDb

If your local SQL Server instance differs, adjust the connection string accordingly.
#### 5. Create / update the database
The project uses Entity Framework Core migrations.

Depending on your setup, create or update the database using either:

- the Package Manager Console in Visual Studio
- the dotnet ef CLI

The configuration files will automatically seed some entries into the DB:

- **an admin account**
  - Nickname: admin
  - E-Mail: admin@email.com
  - Password: Admin@1234
- **a user account**
  - Nickname: user
  - E-Mail: user@email.com
  - Password: User@12345
- default entries for unknown styles, genres, etc
- some test data

If you want to change a users role, you will have to do that directly on the database, for example with SQL Server Management Studio.
#### 6. Start the application
- Configure the startup profile as 'Multiple startup projects'
- Set the projects '1.2_Server' and '1.1_Client' as startup projects

Run the solution from Visual Studio using the configured startup profile.

The application is intended to run entirely on localhost, with the client calling the backend API over HTTPS.

---

## Authentication Flow
BandScope uses JWT-based authentication:

1. The user logs in with email and password
2. The backend validates the credentials
3. The backend generates a JWT
4. The client stores the token locally
5. Authenticated requests automatically include the token in the Authorization header

The frontend also contains logic to detect expired tokens and redirect the user back to the login page.

---

## Notes on Configuration and Secrets
This repository intentionally includes the local appsettings.json configuration.

That is not because I consider committing secrets to be good production practice.
It is included deliberately for documentation and reproducibility reasons:

- The IPA requirement was localhost execution only
- Public deployment was not required
- The repository is meant to show how the project is structured and configured
- The included configuration makes it easier to understand and reproduce the original development setup

If this project were to be prepared for a real deployment, secrets and API keys would of course be moved to a secure secret-management approach and would not be included in source control.

---

## Disclaimer
This repository reflects the state of the project within the scope and time constraints of an IPA.
The focus was on delivering a complete, well-structured, locally executable application within the formal requirements of the assignment.

It should therefore be understood as a documented practical project, not as a production-ready SaaS product.

---

### Author
Claudio Wanner
