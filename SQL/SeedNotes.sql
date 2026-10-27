/* Optional SQL Server notes. The application creates roles and initial SuperAdmin via RoleSeeder. */
-- To inspect seeded tables after running migrations:
-- SELECT Name FROM sys.tables ORDER BY Name;
-- SELECT Id, Email, UserName FROM dbo.AspNetUsers;
-- SELECT Name FROM dbo.AspNetRoles;
-- Do not store plain-text passwords in SQL scripts. */