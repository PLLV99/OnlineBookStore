# 📚 Online Book Store

A small online bookstore built with **ASP.NET Core MVC**. You can sign up, log in, browse books, buy one, and see your past orders.

Made as a two-person project for Humber College (December 2025).

## What it does

- **Sign up & log in**: accounts are handled by ASP.NET Identity (passwords are hashed, login uses a cookie)
- **Book catalogue**: add, edit, delete and list books
- **Buy a book**: pick a book → it's kept in the session → confirm page shows 13% tax and the total → place the order
- **Order history**: see everything you've bought
- **Account details**: update your name and address
- Pages that need a login are protected with `[Authorize]`

## Built with

| | |
|---|---|
| Framework | ASP.NET Core MVC (.NET 9) |
| Database | SQL Server (LocalDB) |
| ORM | Entity Framework Core (code-first migrations) |
| Auth | ASP.NET Core Identity |
| State | Session + cookies |

## Data model

```
AppUser (Identity user + Name, Address)
   │ 1
   │
   │ *
Order ──────── * ── 1 Book
(OrderNum, Total, OrderDate)   (ISBN, Name, Author, Genre, PublishedYear, Price)
```

Three books are seeded automatically so the store isn't empty on first run.

## Run it

You need the [.NET 9 SDK](https://dotnet.microsoft.com/download) and SQL Server LocalDB (comes with Visual Studio).

```bash
git clone https://github.com/PLLV99/OnlineBookStore.git
cd OnlineBookStore/OnlineBookStore
dotnet tool install --global dotnet-ef   # skip if you already have it
dotnet ef database update               # creates the database + seed books
dotnet run
```

Then open the URL shown in the terminal, register an account, and start shopping.

> Using Visual Studio? Open `OnlineBookStore.sln`, run `Update-Database` in the Package Manager Console, then press F5.

## Who did what

- **[@PLLV99](https://github.com/PLLV99)**: built the app end to end: Identity sign-up/login, book CRUD, session checkout, EF Core data model, initial migration and seed data
- **[@florian7610](https://github.com/florian7610)**: added the account details page, order history, and the layout/styling update

## If I built it again

- A real cart stored in the database, so you can buy several books at once and it survives a session timeout
- Stronger password rules (they're relaxed right now to make testing easier)
- Admin role so only admins can add, edit or delete books
- Quantity and stock tracking on orders
