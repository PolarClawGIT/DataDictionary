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

## License

[MIT](https://choosealicense.com/licenses/mit/)

## Copywrite
© 2023 William Howard, All Rights Reserved