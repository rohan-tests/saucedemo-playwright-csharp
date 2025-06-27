⚠️ Note: This project was originally uploaded from another GitHub account which has now been deactivated. Re-uploaded here for portfolio continuity.
# ⚡ Playwright C# – SauceDemo Automation

This project automates the same test scenarios on [saucedemo.com](https://www.saucedemo.com) using **Playwright for .NET**.

## ✅ Features
- Login, cart, and checkout flows
- Add/remove item validations
- Built-in parallel execution
- Auto-wait handling for flaky DOM
- Locator strategies using `getByRole()`, `getByText()`

## 🛠 Tech Stack
- Playwright for .NET
- C#
- NUnit
- .NET Core

## 🚀 How to Run
```bash
dotnet restore
dotnet test
```

## 📁 Notes
- Includes `.gitignore` for reports, binaries, and traces
- Part of a comparison study with Selenium - [Selenium repo here](https://github.com/rohan-tests/saucedemo-selenium-csharp.git)
