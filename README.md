# **CasaDanaAPI** 🚀  
_A modern reservation API built with .NET 9 & PostgreSQL_

## **📌 Overview**
CasaDanaAPI is a **.NET 9 Web API** designed for a reservation system. It follows **clean architecture principles** and uses **Entity Framework Core** with PostgreSQL.

## **📂 Project Structure**
```
📂 casa-dana-api/
│── 📂 src/
│   ├── 📂 CasaDanaAPI/  # Main API project
│       ├── Controllers/  # API endpoints
│       ├── Data/  # Database context
│       ├── DTO/  # Data Transfer Objects
│       ├── Migrations/  # EF Core migrations
│       ├── Models/  # Database models
│       ├── Properties/
│       ├── Repositories/  # Data access layer
│       ├── Services/  # Business logic
│       ├── Program.cs  # Entry point
│── .env  # Environment variables (not tracked in Git)
│── .gitignore
│── appsettings.json  # Configuration
│── appsettings.Development.json  # Dev config
│── casa-dana-api.http  # API request samples
│── docker-compose.yml  # Docker setup
│── README.md  # Project documentation
```
---

## **🚀 Getting Started**
### **1️⃣ Install Dependencies**
Make sure you have:
- [**.NET 9 SDK**](https://dotnet.microsoft.com/)
- [**Docker**](https://www.docker.com/)
- [**PostgreSQL**] (if not using Docker)

### **2️⃣ Clone the Repository**
```sh
git clone https://github.com/your-repo/casa-dana-api.git
cd casa-dana-api
```

### **3️⃣ Set Up Environment Variables**
Create a **`.env`** file in the root directory:
```
PGHOST=localhost
PGPORT=5432
PGDATABASE=ASK_FOR_IT
PGUSER=ASK_FOR_IT
PGPASSWORD=ASK_FOR_IT
```

### **4️⃣ Run PostgreSQL with Docker**
```sh
docker-compose up -d
```
✅ PostgreSQL will now be running on `localhost:5432`.

### **5️⃣ Apply Database Migrations**
```sh
dotnet ef database update --project src/CasaDanaAPI/
```
✅ This will create the necessary tables in PostgreSQL.

### **6️⃣ Run the API**
```sh
dotnet run 
```
✅ The API will start on `http://localhost:5243/`.

## **🛠 Development**
### **Run in Watch Mode**
```sh
dotnet watch run 
```
✅ The server will restart automatically when you make changes.

---

## **📦 Deployment**
### **1️⃣ Build and Run with Docker**
```sh
docker-compose up --build
```
✅ This starts both the API and database inside **Docker containers**.

---

## **🔒 Security Best Practices**
- Never push **`.env`** files to Git.
- Use **JWT authentication** (coming soon).
- Enable **CORS** if deploying for public use.

---

## **👥 Contributors**
- **[Loan CLERIS](https://github.com/TheHikuro)** - Lead Developer

Want to contribute? Feel free to **fork the project** and submit a PR! 🎉

---

## **📜 License**
📄 This project is licensed under the **MIT License**.

---

## **🔗 Useful Links**
- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/)
- [Docker Documentation](https://docs.docker.com/)
