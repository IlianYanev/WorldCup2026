# World Cup 2026 Tournament & Prediction System

An advanced, full-stack web application built using **ASP.NET Core MVC** designed to manage the comprehensive schedule, real-time standings, and knockout brackets of the FIFA World Cup 2026. The platform features an administrator control panel for score entry and phase finalization alongside an interactive user prediction game backed by a dynamic global leaderboard.

---

## 👤 Developer Information
* **Name:** Ilian Yanev
* **Faculty Number:** F115564
* **Role:** Lead Software Developer

---

## 🚀 Key Features & Functionalities

### 1. User Authentication & Role-Based Access Control (RBAC)
* **Secure Authentication:** Identity management utilizing secure password hashing.
* **User Role:** Regular users can log in, simulate tournament progression, predict exact group standings, choose top-3 podium teams, and track their score against others.
* **Admin Role:** Administrators possess exclusive rights to modify match scores across all tournament phases, execute batch saves, and finalize phases to trigger the automatic generation of subsequent knockout rounds.

### 2. Comprehensive Match Management & Schedule
* **Chronological Timeline:** Complete coverage of all 104 matches, dynamically organized into clear visual phases (Group Stage, Round of 32, Round of 16, Quarter-Finals, Semi-Finals, 3rd Place Play-off, and Final).
* **Smart Phase Visualizers:** Group match cells adapt matching group header colors while knockout matches display highly specific phase badges natively calculated by Match IDs.
* **Batch Administration:** Administrators can modify multiple fields at once using specialized view models and conclude an entire round with a single click.

### 3. Dynamic Standings & Statistical Engines
* **Isolated Group Standings:** Points, goal differences, and rankings are evaluated dynamically on-the-fly. The calculation engine isolates matches with `Id <= 72` to safeguard group metrics from getting distorted by knockout stage results.
* **Podium Display:** Concluding the final match instantly transitions the system into the final tournament state, displaying a graphical podium celebrating the Gold (🥇), Silver (🥈), and Bronze (🥉) medalists.

### 4. Interactive Predictor Engine
* **Group Phase Predictions:** Users can sort and predict the precise arrangement of teams within each group (`PredictedPosition` 1 to 4).
* **Podium Predictions:** Users lock in their predictions for the exact top 3 nations.

### 5. Live Global Leaderboard
Tracks non-administrative accounts and processes competitive positioning scores dynamically based on official match confirmations:
* **Position Match:** **+1 Point** for every team correctly predicted on its exact group position.
* **Perfect Group Bonus:** **+3 Points** additional bonus if an entire group's standings are guessed perfectly (4 out of 4 correct positions).
* **Podium Accuracy:** Heavy points distributed for endgame precision—**+8 Points** for guessing the Winner (1st place), **+5 Points** for the Runner-up (2nd place), and **+3 Points** for the 3rd place nation.

---

## 🛠️ Technologies Used

### Backend Framework
* **.NET 10.0 / ASP.NET Core MVC** (Model-View-Controller architecture)

### Data Access & Storage
* **Entity Framework Core (EF Core):** Code-First workflow, utilizing structured migrations for database scheme deployments.
* **Microsoft SQL Server:** Industrial relational database handling models with cascading restrictions.

### Frontend Technologies
* **Razor Views (CSHTML):** Strong-typed server-side layout engines.
* **Bootstrap 5:** Fluid grid containers, sticky navigation overlays, responsive card designs, and alerts.
* **jQuery & Validation:** Seamless front-end data integrity checks and score limits.
* **Custom Typography:** Integration of sport-oriented custom assets.

---

## 📂 Database Entity Scheme

* **User:** Tracks credentials, emails, and authorization claims.
* **Team:** Stores country designations, distinct flag associations, and designated `GroupLetter` properties.
* **Stadium:** Contains venue specifications including location metrics and seating capacities.
* **Match:** Manages scoring variables, timing intervals, venue mapping, and state values (`Scheduled`, `Finished`).
* **GroupPrediction:** Maps user predictions against specific teams and positions.
* **ThirdPlacePrediction:** Persists ranks assigned by competitive users for podium selections.

<img width="5767" height="3765" alt="WorldCupERDiagram" src="https://github.com/user-attachments/assets/38454cae-7468-4c4b-afdf-cdb19c78f04d" />


---

## 💻 How to Run the Project

### Prerequisites
1. Installed **.NET 10.0 SDK** or later.
2. Local database engine (**MS SQL Server LocalDB** or standard **SQL Server instance**).

### Execution Steps

**1. Clone the Repository**
`bash
git clone <repository-url>
cd WorldCup2026
`

**2. Configure Database Connection**
Open `appsettings.json` and adjust the connection string to match your SQL Server setup:
`json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=WorldCup2026Db;Trusted_Connection=True;MultipleActiveResultSets=true"
}
`

**3. Apply Database Migrations**
Execute the following command inside the Package Manager Console (Visual Studio) or via terminal to instantiate the database tables and default lookup values:
`bash
dotnet ef database update
`

**4. Run the Web Application**
Start the hot-reload development server:
`bash
dotnet watch
`
*Open your browser and navigate to the default address (usually `https://localhost:5053` or check your terminal output console).*

**5. Initial Access Credentials**
The database seeds a default system administrator profile for verification:
* **Username:** `admin`
* **Password:** `123`
