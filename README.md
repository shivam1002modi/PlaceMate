# 🚀 PlaceMate: Your University's Placement Experience Hub

*A centralized and moderated platform for sharing and discovering company-specific interview experiences, helping students prepare for campus placements with confidence.*



## 🎯 The Motive

For any university, the campus placement season is a period of high pressure and scattered information. Juniors often rely on fragmented advice from seniors, leading to repetitive questions and inefficient knowledge transfer. Seniors, busy with their own preparations, find it difficult to share their valuable experiences in a scalable way.

**PlaceMate** was built to solve this problem by creating a centralized, structured, and permanent knowledge base. Our core motives are:

* **For Juniors:** To provide a reliable, on-demand platform where they can access detailed and verified interview experiences. Instead of asking repetitive questions, they can learn from a structured format, allowing them to focus their preparation on specific companies and technologies.

* **For Seniors:** To offer a simple, "share-it-once" platform to help their juniors. By submitting their experience, they contribute to a lasting resource, saving them time and ensuring their knowledge benefits the entire college community.

* **For the College:** To improve the overall quality of placements by empowering students with better preparation tools. The platform also serves as a valuable dataset, offering insights into what top companies expect, which helps align the curriculum with industry demands and ultimately boosts the institution's reputation.

---

## ✨ Key Features Implemented

The application is divided into two main roles, each with a rich set of features:

### 🧑‍🎓 For Students

* 🔐 **Secure Login:** Access the platform with a secure account provided by the college administrator.
* 🔍 **Advanced Search & Filtering:** Instantly find relevant experiences by searching for keywords or filtering by Company, Job Role, and Year of Interview.
* 📝 **Structured Experience Submission:** Share interview experiences using a detailed form that captures specific details like technical questions, HR questions, tips, and interview difficulty.
* 👀 **Clear & Organized View:** Browse all approved experiences in a clean, card-based layout that presents information in an easy-to-digest format.

### 👑 For Admins

* 📊 **Insightful Dashboard:** Get a bird's-eye view of the platform with real-time statistics, including total submissions, pending approvals, and the most active companies.
* 🏢 **Full Company Management (CRUD):** Easily Add, View, Edit, and Delete company profiles to keep the database up-to-date.
* 👥 **User Creation:** Create and manage accounts for students and other administrators to maintain a closed and trusted ecosystem.
* ✅ **Moderation Queue:** Review every new submission to ensure quality and accuracy before it's published, with simple one-click "Approve" or "Reject" actions.

---

## 🛠️ Technology Stack

This project was built using a modern, robust technology stack:

| Category     | Technology                                                               |
| ------------ | ------------------------------------------------------------------------ |
| **Backend** | C# on ASP.NET Core 3.1                                                   |
| **Database** | PostgreSQL                                                               |
| **ORM** | Entity Framework Core                                                    |
| **Frontend** | HTML5, CSS3, Bootstrap                                                   |
| **Security** | ASP.NET Core Identity (Cookie-based Authentication), BCrypt (Password Hashing) |
| **Pattern** | Model-View-Controller (MVC)                                              |

---

## ⚙️ Setup and Installation

To run this project locally, please follow these steps:

1.  **Prerequisites:**
    * .NET Core SDK 3.1
    * PostgreSQL (version 14 or later)
    * Visual Studio 2022 (with the "ASP.NET and web development" workload)
    * Git

2.  **Clone the Repository:**
    ```bash
    git clone [https://github.com/shivam1002modi/PlaceMate.git](https://github.com/shivam1002modi/PlaceMate.git)
    cd PlaceMate
    ```

3.  **Configure the Database Connection:**
    * Open the project solution (`.sln`) in Visual Studio.
    * In the **Solution Explorer**, find and open the `appsettings.json` file.
    * Modify the `DefaultConnection` string with your local PostgreSQL username and password.

4.  **Run Database Migrations:**
    * In Visual Studio, go to **Tools > NuGet Package Manager > Package Manager Console**.
    * Run the following command to apply the database schema:
        ```powershell
        Update-Database
        ```

5.  **Run the Application:**
    * Press the `F5` key or the green "Run" button in Visual Studio to build and launch the project. The application will open in your default web browser.

---

## 🧑‍💻 Developer & Contributions

| Developer Name           | Contributions                                                                                                                             |
| ------------------------ | ----------------------------------------------------------------------------------------------------------------------------------------- |
| **Shivam MayurKumar Modi** | Solo developer responsible for all aspects of the project, including full-stack development, database design, system architecture, and UI/UX implementation. |
