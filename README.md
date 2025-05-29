# DataDictionary
This application is envisioned as a Helper application to manage the meta-data that composes the data dictionary within MS SQL.
Specifically, it manages the content of the extended property of MS_Description.

As part of the application, I really wanted to put to practice many of the concepts I learned about in my time as a professional developer but where never implemented based on time/budget constraints.
This includes use:
- Binding List as a wrapper for a Data Table
- Mediator pattern for broadcasting and responding to events
- Threading to perform background tasks
- Database Application Roles to limit the system such that only the registered application(s) can perform work.
- Database based Help system

The Application is expected to be able to:
- Read the MS_Description from the database
- Read the meta-data about the objects within the database (tables, columns, data types, procedures, views, ...)
- Build a Data Dictionary Domain Model (the data) from the data listed above
- Edit the data
- Save the data to a repository within a database (not necessary the source database)
- Write the MS_Description back to the database
- Write script file for use in Visual Studio Database Projects (maybe one day be able to read it)
- Provide a view of the database with meta data and MS_Description


## ARM Notes (as of 5/2025)
This application can be run on a Windows 11/ARM64 system.
But the LocalDB does not work as expected (known issue reported in GitHub).
In native ARM64 CPU, the OS cannot resolve the Path (TCP/IP or Named Pipes).
This is a known issue and thus far Microsoft is not addressing it.

SSDT tools also do not work as expected (known issue reported in GitHub).
It installs and editing can be done but cannot connect to a LocalDb instance (including its own)
This is the same issue as the OS does not start or connect to the LocalDb.
It is possible to make the connection, if the LocalDb is started manually.
The key appears be to use the Pipe name (use "SQLLocalDb i MSSQLLocalDb")
explicitly in the connection string
(example "np:\\.\pipe\LOCALDB#{Instance#}\tsql\query").

SSMS21 also does not currently work on an ARM64 system (known issue).
It will install but will not launch.
The Event Log will show an application crash point to the same issue noted above.

To get the application to run with LocalDb, it needs to be compiled for X86 (not any CPU).
This will cause the ARM system to run the application in emulation mode.
LocalDb then starts and works as expected.

These known issues have been around for several years but no apparent movement on them.

Otherwise, the application is limited to off-line mode.

Base information https://intellitect.com/blog/sql-server-localdb-windows-11-arm-net/

## License

[MIT](https://choosealicense.com/licenses/mit/)

## Copywrite
© 2025 William Howard, All Rights Reserved