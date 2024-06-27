# C# API Project

### Clone the repository to your local machine using the following command:

```sh
git clone https://github.com/ardac67/case_case.git
cd target_folder
dotnet restore
dotnet ef database update
dotnet run

> **Note:** This project requires .NET SDK version 6.0 or higher. Ensure you have the correct version installed before proceeding.
> **Note:** You need to define .env file for necessary parts of the project ex: Connection string , endpoints etc...
> **Note:** Project live at https://case-case.onrender.com but can be work slowly if not connected in a while can take a lot of time because i have free account.
> **Documentation:** https://documenter.getpostman.com/view/28660570/2sA3dsnZqr.

### Running with Docker
> **Note:** You need to define .env file for necessary parts of the project for docker ex: Connection string , endpoints etc...
> **Note:** Build command creates a image for both db and the app can be configured in yml and Dockerfile if needed...
```sh
docker-compose up --build

