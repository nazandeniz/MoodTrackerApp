# 🌙 MoodTrackerApp

A simple and elegant **Mood Tracking App** built with **C# (.NET 9, WinForms, Entity Framework Core, and SQLite)**.  
It allows you to record your daily mood, energy levels, and notes — and visualize your emotional trends over time.

---

## 🖥️ Features

✅ Record daily mood and energy levels (1–10 scale)  
✅ Add tags like *Work*, *Study*, *Relax*, *Social*, *Health*, etc.  
✅ Save short notes for each entry  
✅ View all entries in a clean **DataGridView**  
✅ Visualize mood & energy trends in a **Chart (line graph)**  
✅ SQLite database with EF Core repository pattern  
✅ Automatic database creation on first run  

---

## 🧱 Tech Stack

| Component | Description |
|------------|-------------|
| **.NET 9** | Latest .NET version for desktop apps |
| **WinForms** | Modern UI for Windows desktop |
| **Entity Framework Core** | ORM for SQLite database |
| **SQLite** | Local lightweight database |
| **C#** | Main programming language |

---

## ⚙️ Setup Instructions

### 🔹 Requirements
- Visual Studio 2022 (or newer)
- .NET 9 SDK
- Git installed

### 🔹 Installation Steps
1. **Clone the repository:**
   ```bash
   git clone https://github.com/nazandeniz/MoodTrackerApp.git
Open the solution in Visual Studio

Open MoodTrackerApp.sln

Build and Run

Visual Studio → ▶️ “Start Debugging”

On first launch, the SQLite database (app.db) will be auto-created.

Add new entries

Enter Date, Mood (1–10), Energy (1–10), Tag, and Note

Click Save

View trends

Check the Chart to see mood and energy over time.

<img width="1918" height="1013" alt="image" src="https://github.com/user-attachments/assets/c40ddfcf-8267-4d08-9164-bf7292b58e14" />
