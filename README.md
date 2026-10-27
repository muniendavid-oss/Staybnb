# Staybnb — PROGRAMMING 742

ASP.NET Core MVC + ASP.NET Core Identity + EF Core + SQL Server reference implementation for the supplied assignment brief.

## Requirements
- .NET 8 SDK
- SQL Server Express or LocalDB
- VS Code with C# Dev Kit (recommended)

## Run locally
1. Install the .NET 8 SDK and SQL Server Express/LocalDB.
2. Open this folder in VS Code.
3. Review `appsettings.json`. The default connection uses Windows LocalDB. For SQL Server Express, change it to e.g. `Server=localhost\SQLEXPRESS;Database=StaybnbDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true`.
4. In a PowerShell terminal in this folder, either run `./setup.ps1` or run these commands:
   ```bash
   dotnet restore
   dotnet tool install --global dotnet-ef --version 8.0.11
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   dotnet run
   ```
5. Open the localhost HTTPS URL printed by `dotnet run`.

The application attempts database migration and role seeding on startup. The explicit EF commands above are recommended for a first setup and reproducible migration files.

## Initial SuperAdmin
For first local run, the seeder creates `superadmin@staybnb.local` with password `ChangeMe123!` if no environment variables are set. **Immediately change this password and set `STAYBNB_ADMIN_EMAIL` and `STAYBNB_ADMIN_PASSWORD` before any shared/deployed use.** Never deploy the development credentials. Role seeding also creates Guest, Host, Admin and SuperAdmin roles.

PowerShell example for a local custom seed account (set before running):
```powershell
$env:STAYBNB_ADMIN_EMAIL = "your-admin@example.com"
$env:STAYBNB_ADMIN_PASSWORD = "Use-A-Unique-Strong-Password-Here"
dotnet run
```

## Main features included
- Identity registration/login, password policy, lockout and activity logging.
- New accounts receive Guest role.
- Guest host application with required image upload; Admin approval removes Guest and adds Host.
- Host property activation controls and booking request review.
- Guest property search by city/type and booking price calculation: `(nightly rate × nights) + cleaning fee + service fee`.
- Guest document upload for approved bookings, private storage outside `wwwroot`, host verification and check-in status.
- SuperAdmin-only Guest-to-Admin promotion; admin activity log.
- Database-backed inbox, read state and notifications.

## Security and project notes
- Uploaded property images are restricted by extension and size. For production, also inspect file signatures, use malware scanning, and store images in object storage.
- Guest ID/passport documents are stored in `PrivateUploads` outside the public web root and the database stores an opaque generated filename. This demo includes no download endpoint; add a separately authorized document viewer before production use.
- Configure HTTPS, secret management, backups, retention policies, and a proper administrator account before deployment.
- The starter project is not a commercial-ready Airbnb replacement. Test every rubric workflow on your machine and complete any institution-specific report/declaration yourself.

## Suggested acceptance tests
1. Register a normal user and confirm Guest role.
2. Try opening Host/Admin URLs as Guest; confirm access is denied.
3. Apply to host without an image; submission must fail. Submit with a valid image.
4. Sign in as Admin, approve application, then verify Guest role is removed and Host role is added.
5. As Host, activate/deactivate listing and approve/reject booking requests.
6. As Guest, request dates and verify total price and overlapping-date rejection.
7. Upload a document, verify it as host/admin, then complete check-in.
8. Send a message, read notifications and verify ActivityLog entries.
9. Sign in as SuperAdmin and promote an eligible Guest to Admin.

## Important limitations to address before submission
- EF migration source files are generated on your machine using `dotnet ef migrations add InitialCreate` because the code-generation environment does not include the .NET SDK. Commit the generated `Migrations/` folder into your submission.
- Build and run tests locally. Resolve compiler/runtime issues, then zip the complete folder excluding `bin/` and `obj/`.
- The supplied brief mentions a multi-step check-in workflow; this implementation includes document upload, host verification, and checked-in status, but your lecturer may expect additional property-specific check-in instructions in the guest screen.
