
# C# API Project

### Clone the repository to your local machine using the following command:

```sh
git clone https://github.com/ardac67/case_case.git
cd target_folder
dotnet restore
dotnet ef database update
dotnet run
```

> **Note:** This project requires .NET SDK version 6.0 or higher. Ensure you have the correct version installed before proceeding.
> 
> **Note:** You need to define an `.env` file for necessary parts of the project such as the connection string, endpoints, etc.
> 
> **Note:** Project live at [https://case-case.onrender.com](https://case-case.onrender.com) but can be slow if not accessed for a while due to free account limitations.
> 
> **Documentation:** [https://documenter.getpostman.com/view/28660570/2sA3dsnZqr](https://documenter.getpostman.com/view/28660570/2sA3dsnZqr).
>
> **Note:** There is a service inside utils folder that gets the upcoming movies from 12 hour based interval and insert them into db.
>

### Running with Docker

> **Note:** You need to define an `.env` file for necessary parts of the project for Docker such as the connection string, endpoints, etc.
> 
> **Note:** The build command creates an image for both the database and the app, and can be configured in the `docker-compose.yml` and `Dockerfile` if needed.
```sh
docker-compose up --build
```
